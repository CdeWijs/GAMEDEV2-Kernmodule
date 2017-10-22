using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour {

    public GameObject tailPrefab;

    private float speed;
    
    private bool ateCandy = false;
    private bool startMoving = false;
    private Vector2 direction = Vector2.right;
    private List<Transform> tail = new List<Transform>();
    private AudioSource audioSource;

    private CandySpawn candySpawn;
    private LevelManager levelManager;
    private LevelGenerator levelGenerator;
    
	void Start () {
        audioSource = GetComponent<AudioSource>();

        candySpawn = FindObjectOfType<CandySpawn>();
        levelManager = FindObjectOfType<LevelManager>();
        levelGenerator = FindObjectOfType<LevelGenerator>();

        // easy
        if (PlayerPrefsManager.GetDifficulty() == 0f) {
            speed = 12f;
        }
        // normal
        else if (PlayerPrefsManager.GetDifficulty() == 1f) {
            speed = 14f;
        }
        // hard
        else {
            speed = 16f;
        }
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

        // If generator is done drawing the borders, start moving snake
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
            Score.score++;
            ateCandy = false;
        }
        else if (tail.Count > 0) {
            // Move last pixel to where the head was
            tail.Last().position = currentPosition;

            // Add last pixel to front of list, remove from back.
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }

        for (int i = 0; i < tail.Count - 1; i++) {
            if (i <= 4) {
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

            audioSource.Play();
            candySpawn.SetInActive(collision.gameObject);
        }
        else {
            Debug.Log(collision);
            PlayerPrefsManager.SetHighScore(Score.score);
            levelManager.LoadLevel("Game Over");
        }
    }
}
