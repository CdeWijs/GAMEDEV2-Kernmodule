using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge3 : MonoBehaviour {

    private int prevNumber = 0;
    private int nextNumber = 1;
    
	void Start () {

		for (int i = 0; i < 100; i++) {
            int x = prevNumber;

            prevNumber = nextNumber;
            nextNumber += x;
            Debug.Log(nextNumber);
        }
	}
}
