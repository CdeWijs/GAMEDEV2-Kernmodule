using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButterflyPickup : MonoBehaviour {
	
	public AudioSource aSource;
	public GameObject butterfly;
	public GameObject butterflyCounter;
	public GameObject pressE;
	public int scoreValue = 1;

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
