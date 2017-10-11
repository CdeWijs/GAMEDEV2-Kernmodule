using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour {

    public GameObject tailPrefab;
    public float speed = 10f;
    public GameObject candySpawner;

    private bool ateCandy = false;
    private Vector2 direction = Vector2.right;
    private List<Transform> tail = new List<Transform>();
    private CandySpawn candySpawn;
    private LevelManager levelManager;
    
	void Start () {
        candySpawn = FindObjectOfType<CandySpawn>();
        levelManager = FindObjectOfType<LevelManager>();
        candySpawn.Spawn();
	}
	
	void FixedUpdate () {
        Move();

        if (Input.GetKey(KeyCode.RightArrow)) {
            direction = Vector2.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            direction = Vector2.left;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            direction = Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            direction = Vector2.down;
        }
	}

    private void Move() {
        Vector2 currentPosition = transform.position;

        transform.Translate(direction * Time.deltaTime * speed, Space.World);

        if (ateCandy == true) {
            candySpawn.Spawn();

            for (int i = 0; i <= 2; i++) {
                GameObject g = (GameObject)Instantiate(tailPrefab, currentPosition, Quaternion.identity);
                tail.Insert(0, g.transform);
            }
                
            ateCandy = false;
        }
        else if (tail.Count > 0) {
            // Move last tail pixel to where the head was
            tail.Last().position = currentPosition;

            // Add last pixel to front of list, remove from back.
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.StartsWith("Candy")) {
            ateCandy = true;

            Destroy(collision.gameObject);
        }
        else {
            levelManager.LoadLevel("Game Over");
        }
    }
}
