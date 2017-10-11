using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	public AudioClip [] levelMusic;

	private AudioSource aSource;

	void Awake () {
		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		aSource = GetComponent<AudioSource>();
		aSource.volume = PlayerPrefsManager.GetMasterVolume ();
		SceneManager.sceneLoaded += OnLevelLoaded;
	}

	void OnLevelLoaded (Scene scene, LoadSceneMode mode) {
		AudioClip audioClip = levelMusic[scene.buildIndex];
		Debug.Log ("Playing clip: " + audioClip);

		if (audioClip) {
			aSource.clip = audioClip;
			aSource.loop = true;
			aSource.Play();
		}
	}

	public void ChangeVolume (float volume){
		aSource.volume = volume;
	}
}
