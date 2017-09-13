using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButterflyPickup : MonoBehaviour {

	public GameObject pressE;
	public GameObject butterfly;
	public GameObject butterflyCounter;
	public int scoreValue = 1;
	public AudioSource aSource;

	private bool isInTrigger = false;


	void Start () {
		pressE.SetActive(false);
	}

	void Update () {

		if (isInTrigger){
			pressE.SetActive(true);

			if (Input.GetKeyDown(KeyCode.E)){
				pressE.SetActive(false);
				aSource.Play();
				ButterflyCounter.score += scoreValue;
				Destroy(gameObject);
			}

		} else {
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.CompareTag("Player")) {
			isInTrigger = true;
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.CompareTag("Player")) {
			isInTrigger = false;
		}
	}
}
