using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorToggleMenu : MonoBehaviour {

	private bool isCursorLocked;

	void Start () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
