using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject objectToSpawn;
	public int numberOfEnemies;

	private float spawnRadius = 20;
	private Vector3 spawnPosition;

	void OnEnable () {
		SpawnObject();
	}

	void OnDisable () {
	}

	void SpawnObject () {
		for (int i = 0; i < numberOfEnemies; i++) {
			spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
			Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
		}
	}
}
