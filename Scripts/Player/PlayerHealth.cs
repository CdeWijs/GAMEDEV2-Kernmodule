using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 200;
	public int currentHealth;
	public Slider healthSlider;
	public AudioSource aSource;

	private LevelManager levelManager;

	bool damaged;

	void Awake () {
		currentHealth = startingHealth;
	}

	void Update () {

	}

	public void TakeDamage (int amount) {
		aSource.Play();
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if (currentHealth <= 0) {
			levelManager = GameObject.FindObjectOfType<LevelManager>();
			levelManager.LoadLevel("Game Over");
		}
	}
}
