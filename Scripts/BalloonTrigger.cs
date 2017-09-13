using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonTrigger : MonoBehaviour {

	public GameObject airBalloon;
	public GameObject firstPersonController;
	public GameObject notEnoughButterflies;
	public Transform risePosition;
	public float speed;

	private bool riseUp = false;


	void Start () {
		notEnoughButterflies.SetActive(false);
	}

	void Update () {

		if (riseUp) {
			airBalloon.transform.position = Vector3.Lerp (airBalloon.transform.position, 
			risePosition.position, speed * Time.deltaTime);
			firstPersonController.transform.position = Vector3.Lerp (firstPersonController.transform.position,
			risePosition.position, speed * Time.deltaTime);
		}
	}

	void OnTriggerStay (Collider col) {
		if (col.CompareTag("Player") && ButterflyCounter.score >= 10) {
			riseUp = true;
		} 
		else {
			riseUp = false;
			notEnoughButterflies.SetActive(true);
		}
	}

	void OnTriggerExit (Collider col) {
		notEnoughButterflies.SetActive(false);
	}
}
