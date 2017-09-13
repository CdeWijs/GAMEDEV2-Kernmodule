using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordHandler : MonoBehaviour {

	public GameObject enemyObject;
	public Collider sword;
	private Animator swordAnim;
	private AudioSource aSource;
	private float coolDown = 1f;
	private float nextHit;

	private Animator rhinoAnim;
	private NavMeshAgent rhinoNav;
	private AudioSource rhinoAudio;

	void Start () {
		swordAnim = GetComponent<Animator>();
		sword = gameObject.GetComponent<Collider> ();
		aSource = GetComponent<AudioSource>();
	}

	void Update () {

		if (Input.GetButtonDown("Fire1") && Time.time > nextHit){
			nextHit = Time.time + coolDown;

			aSource.Play();
			swordAnim.SetBool("swordHit", true);
			sword.enabled = true;
		} else {
			swordAnim.SetBool("swordHit", false);
			sword.enabled =false;
		}

	}

	void OnTriggerEnter (Collider col) {
		if (col.CompareTag("Enemy")){

			rhinoAnim = col.GetComponent<Animator>();
			rhinoNav = col.GetComponent<NavMeshAgent>();
			rhinoAudio = col.GetComponent<AudioSource>();

			rhinoAudio.Play();
			rhinoNav.enabled = false;
			rhinoAnim.SetBool("isDead", true);
			rhinoAnim.SetBool("isWalking", false);
			rhinoAnim.SetBool("isRunning", false);
			rhinoAnim.SetBool("isAttacking", false);

			Destroy(col.gameObject, 10f);
		}
	}
}
