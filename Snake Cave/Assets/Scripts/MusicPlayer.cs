using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;

    private AudioSource music;

    void Start() {
        music = GetComponent<AudioSource>();

        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.loop = true;
            music.Play();
        }
    }

	public void ChangeVolume (float volume){
		music.volume = volume;
	}
}
