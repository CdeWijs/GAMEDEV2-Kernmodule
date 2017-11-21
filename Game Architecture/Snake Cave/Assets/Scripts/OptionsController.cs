using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
    public Slider difficultySlider;

	public LevelManager levelManager;

    public GameObject easy;
    public GameObject normal;
    public GameObject hard;

	private MusicPlayer musicPlayer;

    private void Start () {
		musicPlayer = FindObjectOfType<MusicPlayer>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume ();
        difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}

	private void Update () {
		musicPlayer.ChangeVolume(volumeSlider.value);

        // easy
        if (difficultySlider.value == 0f) {
            easy.SetActive(true);
            normal.SetActive(false);
            hard.SetActive(false);
        }
        // normal
        else if (difficultySlider.value == 1f) {
            easy.SetActive(false);
            normal.SetActive(true);
            hard.SetActive(false);
        }
        // hard
        else {
            easy.SetActive(false);
            normal.SetActive(false);
            hard.SetActive(true);
        }
	}

	public void SaveAndExit () {
		PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
        PlayerPrefsManager.SetDifficulty(difficultySlider.value);
		SceneManager.LoadScene ("Start");
	}

	public void SetDefaults () {
		volumeSlider.value = 0.8f;
        difficultySlider.value = 1f;
	}
}
