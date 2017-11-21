using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteriousNote : MonoBehaviour {
	
	public GameObject mysteriousNote;
	public GameObject readNote;

	private bool isInTrigger = false;


	void Start () {
	}

	void Update () {

		if (isInTrigger){

			if (Input.GetKeyDown(KeyCode.E)){
				readNote.SetActive(false);
				mysteriousNote.SetActive(true);
				readNote.SetActive(false);
				Time.timeScale = 0;
			} 

			if (Input.GetKeyDown(KeyCode.Z)){
				mysteriousNote.SetActive(false);
				Time.timeScale = 1;
				readNote.SetActive(true);
			}

		} 
		else {
			readNote.SetActive(false);
			mysteriousNote.SetActive(false);
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.CompareTag("Player")) {
			isInTrigger = true;
			readNote.SetActive(true);
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.CompareTag("Player")) {
			isInTrigger = false;
		}
	}
}
