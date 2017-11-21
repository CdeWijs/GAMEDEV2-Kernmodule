using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;

	void Awake () {

		if (instance != null) {
			Destroy (gameObject);
		} 
		else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	void Update () {
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;

			if (sceneName == "Game"){
			GameObject.Destroy(gameObject);
		}
	}
}
