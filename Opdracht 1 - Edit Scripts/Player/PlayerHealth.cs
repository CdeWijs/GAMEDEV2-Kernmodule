using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public AudioSource aSource;
	public int currentHealth;
	public int startingHealth = 200;
	public Slider healthSlider;

	private LevelManager levelManager;
	private bool damaged;

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
