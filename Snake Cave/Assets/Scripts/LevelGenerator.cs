using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject tile;
    public List<Vector3> createdTiles;

    public int tileAmount;
    public int tileSize;

    public float chanceUp;
    public float chanceRight;
    public float chanceDown;

    public float drawTime = 0.1f;
    public bool isDrawn = false;

    public float minY = 99999f;
    public float maxY = 0f;
    public float minX = 99999f;
    public float maxX = 0f;

    private float minScreenY = -40f;
    private float maxScreenY = 40f;
    private float minScreenX = -60f;
    private float maxScreenX = 60f;

    private int iterator = 0;
    
	void Awake () {
        StartCoroutine(GenerateLevel());
	}

    IEnumerator GenerateLevel() {
        do {
            float direction = Random.Range(0f, 1f);

            CreateTile();
            CallMoveGenerator(direction);
            SetValues();
            iterator++;

            yield return new WaitForSeconds(drawTime);
        } while (iterator < tileAmount);

        if (iterator >= tileAmount) {
            isDrawn = true;
        }

        yield return 0;
    }

    private void Update() {
    }

    void CallMoveGenerator(float randomDirection) {
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

    void MoveGenerator(int direction) {
        switch (direction) {
            case 0:
                transform.position = new Vector3(transform.position.x, transform.position.y + tileSize, 0);
                break;

            case 1:
                transform.position = new Vector3(transform.position.x + tileSize, transform.position.y, 0);
                break;

            case 2:
                transform.position = new Vector3(transform.position.x, transform.position.y - tileSize, 0);
                break;

            case 3:
                transform.position = new Vector3(transform.position.x - tileSize, transform.position.y, 0);
                break;
        }
    }

    void CreateTile() {
        if (!createdTiles.Contains(transform.position)) {
            GameObject tileObject = Instantiate(tile, transform.position, transform.rotation) as GameObject;
            createdTiles.Add(tileObject.transform.position);
        }
        else if (transform.position.x >= minScreenX && transform.position.x <= maxScreenX) {
            if (transform.position.y >= minScreenY && transform.position.y <= maxScreenY) {
                GameObject tileObject = Instantiate(tile, transform.position, transform.rotation) as GameObject;
                createdTiles.Add(tileObject.transform.position);
            }
        }
        else {
            tileAmount++;
        }
    }

    void SetValues() {
        for (int i = 0; i < createdTiles.Count; i++) {
            if (createdTiles[i].y < minY) {
                minY = createdTiles[i].y;
            }
            if (createdTiles[i].y > maxY) {
                maxY = createdTiles[i].y;
            }
            if (createdTiles[i].x < minX) {
                minX = createdTiles[i].x;
            }
            if (createdTiles[i].x > maxX) {
                maxX = createdTiles[i].x;
            }
        }
    }
}
