using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour {

	private LevelManager levelManager;

	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

	void OnTriggerStay (Collider col) {
		if (col.CompareTag("Player")) {
			levelManager.LoadLevel("Win");
		}
	}
}
