using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject tile;
    public List<Vector3> createdTiles;

    public int tileAmount;
    public int tileSize;

    // Chances of the stated directions
    public float chanceUp;
    public float chanceRight;
    public float chanceDown;

    // Generating the border at the start of the game
    public float drawTime = 0.1f;
    public bool isDrawn = false;

    private int iterator = 0;
    private Vector3 startPosition;
    private bool stopGenerator = false;
    
    // Min and max values of the created tiles.
    public float minX = 9999f;
    public float maxX = 0f;
    public float minY = 9999f;
    public float maxY = 0f;

    // Min and max values of the screen (used for corners).
    private float minScreenX;
    private float maxScreenX;
    private float minScreenY;
    private float maxScreenY;
    
    private void Awake() {
        // easy
        if (PlayerPrefsManager.GetDifficulty() == 0f) {
            startPosition = new Vector3(-39, 25, -5);
            minScreenX = -40f;
            maxScreenX = 40f;
            minScreenY = -25f;
            maxScreenY = 25f;
        }
        // normal
        else if (PlayerPrefsManager.GetDifficulty() == 1f) {
            startPosition = new Vector3(-29, 20, -5);
            minScreenX = -30f;
            maxScreenX = 30f;
            minScreenY = -20f;
            maxScreenY = 20f;
        }
        // hard
        else {
            startPosition = new Vector3(-24, 14, -5);
            minScreenX = -25f;
            maxScreenX = 25f;
            minScreenY = -15f;
            maxScreenY = 15f;
        }

        transform.position = startPosition;
        StartCoroutine(GenerateLevel());
    }

    private IEnumerator GenerateLevel() {

        do {
            float direction = UnityEngine.Random.Range(0f, 1f);

            CreateTile();
            CallMoveGenerator(direction);
            iterator++;

            yield return new WaitForSeconds(drawTime);
        } while (iterator < tileAmount);

        if (iterator >= tileAmount) {
            isDrawn = true;
        }

        yield return 0;
    }

    private void CallMoveGenerator(float randomDirection) {
        CreateCorner();

        if (randomDirection < chanceUp) {
            MoveGenerator(0);
        }
        else if (randomDirection < chanceRight) {
            MoveGenerator(1);
        }
        else if (randomDirection < chanceDown) {
            MoveGenerator(2);
        }
        else {
            MoveGenerator(3);
        }
    }

    private void MoveGenerator(int direction) {
        switch (direction) {
            // up
            case 0:
                transform.position = new Vector3(transform.position.x, transform.position.y + tileSize, 0);
                break;
            // right
            case 1:
                transform.position = new Vector3(transform.position.x + tileSize, transform.position.y, 0);
                break;
            // down
            case 2:
                transform.position = new Vector3(transform.position.x, transform.position.y - tileSize, 0);
                break;
            // left
            case 3:
                transform.position = new Vector3(transform.position.x - tileSize, transform.position.y, 0);
                break;
        }
    }

    private void CreateTile() {
        // If there already is a tile on this position, add one iteration.
        if (createdTiles.Contains(transform.position)) {
            tileAmount++;
        }
        else {
            GameObject tileObject = Instantiate(tile, transform.position, transform.rotation) as GameObject;
            createdTiles.Add(tileObject.transform.position);
        }
    }

    private void CreateCorner() {

        // top right corner
        if (transform.position.x > maxScreenX) {
            chanceUp = 0f;
            chanceRight = 0.2f;
            chanceDown = 0.8f;
        }
        // bottom right corner
        if (transform.position.y < minScreenY) {
            chanceUp = 0.2f;
            chanceRight = 0;
            chanceDown = 0.4f;
        }
        // bottom left corner
        if (transform.position.x < minScreenX) {
            chanceUp = 0.6f;
            chanceRight = 0.85f;
            chanceDown = 0;
            stopGenerator = true;
        }
        // top left corner/end
        if (stopGenerator && transform.position.y >= maxScreenY) {
            StopAllCoroutines();
            SetValues();
            isDrawn = true;
        }
    }

    private void SetValues() {
        // Set values for min and max tiles. Candies spawn between these.
        for (int i = 0; i < createdTiles.Count - 1; i++) {
            if (createdTiles[i].x < minX) {
                minX = createdTiles[i].x;
            }
            else if (createdTiles[i].x > maxX) {
                maxX = createdTiles[i].x;
            }
            else if (createdTiles[i].y < minY) {
                minY = createdTiles[i].y;
            }
            else if (createdTiles[i].y > maxY) {
                maxY = createdTiles[i].y;
            }
        }
    }
}
