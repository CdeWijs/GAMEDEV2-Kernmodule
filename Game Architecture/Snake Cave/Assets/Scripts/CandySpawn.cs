using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawn : MonoBehaviour {

    public GameObject candy;

    private int pooledAmount;
    private List<GameObject> candies = new List<GameObject>();
    private LevelGenerator levelGenerator;
    private float offset = 5f;
    private float time;


    private void Awake() {
        // easy
        if (PlayerPrefsManager.GetDifficulty() == 0f) {
            pooledAmount = 10;
        }
        // normal
        else if (PlayerPrefsManager.GetDifficulty() == 1f) {
            pooledAmount = 8;
        }
        // hard
        else {
            pooledAmount = 5;
        }

        // Create object pool.
        for (int i = 0; i < pooledAmount; i++) {
            GameObject gObject = Instantiate(candy);
            gObject.SetActive(false);
            candies.Add(gObject);
        }
    }

    private void Start() {
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    private void Update() {
        time += Time.deltaTime;

        if (time >= 10) {
            for (int i = 0; i < candies.Count - 1; i++) {
                candies[i].SetActive(false);
                Spawn();
            }
        }
    }

    public void Spawn() {
        time = 0;

        // Get min and max values.
        float minX = levelGenerator.minX;
        float maxX = levelGenerator.maxX;
        float minY = levelGenerator.minY;
        float maxY = levelGenerator.maxY;
       
        for (int i = 0; i < candies.Count; i++) {

            // Get random values between minX, maxY, minY and maxY.
            // offset makes the chance that candies spawn outside of the border smaller.
            int x = (int)Random.Range(minX + offset, maxX - offset);
            int y = (int)Random.Range(minY + offset, maxY - offset);

            if (!candies[i].activeInHierarchy) {
                // Check if there's no tile on this position.
                if (!levelGenerator.createdTiles.Contains(transform.position)) {
                    candies[i].transform.position = new Vector2(x, y);
                    candies[i].transform.rotation = Quaternion.identity;
                    candies[i].SetActive(true);
                }
                // If there's a tile, add iteration.
                else {
                    i--;
                }
            }
        }
    }

    public void SetInActive(GameObject candy) {
        candy.SetActive(false);
    }
}
