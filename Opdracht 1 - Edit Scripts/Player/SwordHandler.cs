using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordHandler : MonoBehaviour {

	private Animator swordAnim;
	private AudioSource aSource;
	public Collider sword;
	public GameObject enemyObject;
	private float coolDown = 1f;
	private float nextHit;

	private AudioSource rhinoAudio;
	private Animator rhinoAnim;
	private NavMeshAgent rhinoNav;

	void Start () {
		aSource = GetComponent<AudioSource>();
		sword = gameObject.GetComponent<Collider> ();
		swordAnim = GetComponent<Animator>();
	}

	void Update () {

		if (Input.GetButtonDown("Fire1") && Time.time > nextHit){
			nextHit = Time.time + coolDown;

			aSource.Play();
			swordAnim.SetBool("swordHit", true);
			sword.enabled = true;
		} 
		else {
			swordAnim.SetBool("swordHit", false);
			sword.enabled =false;
		}

	}

	void OnTriggerEnter (Collider col) {
		if (col.CompareTag("Enemy")){

			rhinoAnim = col.GetComponent<Animator>();
			rhinoAudio = col.GetComponent<AudioSource>();
			rhinoNav = col.GetComponent<NavMeshAgent>();

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
