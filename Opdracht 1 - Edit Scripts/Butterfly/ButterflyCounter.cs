using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButterflyCounter : MonoBehaviour {

	public static int Score;
	public GameObject goToBalloon;

	private Text text;
	private float timeText = 10;

	void Start () {
		text = GetComponent<Text>();
		score = 0;
	}

	void Update () {
		text.text = score.ToString();

		if (score >= 10) {
			timeText -= Time.deltaTime;
			goToBalloon.SetActive(true);

			if (timeText <= 0) {
				goToBalloon.SetActive(false);
			}
		} 
		else {
			goToBalloon.SetActive(false);
		}
	}
}
