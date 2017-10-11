using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawn : MonoBehaviour {

    public GameObject candyPrefab;

    public Transform borderUp;
    public Transform borderDown;
    public Transform borderLeft;
    public Transform borderRight;

    public void Spawn() {
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        int y = (int)Random.Range(borderDown.position.y, borderUp.position.y);

        Instantiate(candyPrefab, new Vector2(x, y), Quaternion.identity);
    }
}
