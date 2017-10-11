using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyTimer : MonoBehaviour {

    public float allowedTimeActive = 20;

    private float timer;
    private CandySpawn candySpawn;

    void Start() {
        candySpawn = FindObjectOfType<CandySpawn>();
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer >= allowedTimeActive) {
            gameObject.SetActive(false);
            candySpawn.Spawn();
            timer = 0;
        }
    }
}
