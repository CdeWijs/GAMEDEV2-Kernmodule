using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge1 : MonoBehaviour {

    private const int SIZE = 10;

    private List<int> numbers;
    private int sum = 0;
    private int index = SIZE - 1;
    
	void Start () {
        numbers = new List<int>();

        for (int i = 0; i < SIZE; i++) {
            numbers.Add(i);
        }

        CalculateSum();
	}
	
    private void CalculateSum() {

        sum += numbers[index];
        numbers.RemoveAt(index);
        index--;
        PrintSum();

        while (numbers.Count > 0) {
            CalculateSum();
        }
    }

    private void PrintSum() {
        Debug.Log(sum);
    }
}
