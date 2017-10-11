using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider, difficultySlider;
	public LevelManager levelManager;

	private MusicPlayer musicPlayer;

	void Start () {
		musicPlayer = FindObjectOfType<MusicPlayer>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume ();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}

	void Update () {
		musicPlayer.ChangeVolume(volumeSlider.value);
	}

	public void SaveAndExit () {
		PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
		PlayerPrefsManager.SetDifficulty (difficultySlider.value);
		SceneManager.LoadScene ("01a Start");
	}

	public void SetDefaults () {
		volumeSlider.value = 0.8f;
		difficultySlider.value = 2f;
	}
}
