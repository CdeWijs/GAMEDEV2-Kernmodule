using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour {

    public GameObject tailPrefab;
    public float speed = 10f;
    
    private bool ateCandy = false;
    private bool startMoving = false;
    private Vector2 direction = Vector2.right;
    private List<Transform> tail = new List<Transform>();

    private CandySpawn candySpawn;
    private LevelManager levelManager;
    private LevelGenerator levelGenerator;
    
	void Start () {
        candySpawn = FindObjectOfType<CandySpawn>();
        levelManager = FindObjectOfType<LevelManager>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
	}
	
	void FixedUpdate () {

        // Change direction the snake is going
        if (Input.GetKey(KeyCode.RightArrow) && direction != Vector2.left) {
            direction = Vector2.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && direction != Vector2.right) {
            direction = Vector2.left;
        }
        if (Input.GetKey(KeyCode.UpArrow) && direction != Vector2.down) {
            direction = Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow) && direction != Vector2.up) {
            direction = Vector2.down;
        }

        if (levelGenerator.isDrawn == true) {
            candySpawn.Spawn();
            levelGenerator.isDrawn = false;
            startMoving = true;
        }

        if (startMoving == true) {
            Move();
        }
    }

    private void Move() {
        Vector3 currentPosition = transform.position;

        transform.Translate(direction * Time.deltaTime * speed, Space.World);

        // Spawn new candy and instantiate new tail 
        if (ateCandy == true) {
            candySpawn.Spawn();

            GameObject g = Instantiate(tailPrefab, currentPosition, Quaternion.identity);
            tail.Insert(0, g.transform);

            ateCandy = false;
        }
        else if (tail.Count > 0) {
            // Move last pixel to where the head was
            tail.Last().position = currentPosition;

            // Add last pixel to front of list, remove from back.
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }

        // conditions for tail trigger to be true
        for (int i = 0; i < tail.Count - 1; i++) {
            if (tail[i] == tail[0] || tail[i] == tail[1] || tail[i] == tail[2] || tail[i] == tail[3] ) {
                tail[i].GetComponent<BoxCollider2D>().isTrigger = false;
            }
            else if (tail[i] == tail.Last()) {
                tail[i].GetComponent<BoxCollider2D>().isTrigger = false;
            }
            else {
                tail[i].GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.StartsWith("Candy")) {
            ateCandy = true;

            candySpawn.SetInActive(collision.gameObject);
        }
        else {
            Debug.Log(collision);
            levelManager.LoadLevel("Game Over");
        }
    }
}
