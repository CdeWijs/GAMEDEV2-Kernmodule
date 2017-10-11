using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawn : MonoBehaviour {

    public GameObject candy;
    public int pooledAmount = 8;

    public Transform borderUp;
    public Transform borderDown;
    public Transform borderLeft;
    public Transform borderRight;

    private List<GameObject> candies = new List<GameObject>();
    private int safetyNet = 2;

    private void Awake() {
        for (int i = 0; i < pooledAmount; i++) {
            GameObject gObject = Instantiate(candy);
            gObject.SetActive(false);
            candies.Add(gObject);
        }

        
    }

    public void Spawn() {
       
        for (int i = 0; i < candies.Count; i++) {
            int x = (int)Random.Range(borderLeft.position.x + safetyNet, borderRight.position.x - safetyNet);
            int y = (int)Random.Range(borderDown.position.y + safetyNet , borderUp.position.y - safetyNet);

            if (!candies[i].activeInHierarchy) {
                candies[i].transform.position = new Vector2(x, y);
                candies[i].transform.rotation = Quaternion.identity;
                candies[i].SetActive(true);
            }
        }
    }

    public void SetInActive(GameObject candy) {
        candy.SetActive(false);
    }
}
