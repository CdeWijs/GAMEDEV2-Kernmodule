using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	public int attackDamage = 5;
	public LayerMask detectionLayer;

	private Animator anim;
	private AudioSource aSource;
	private GameObject player;
	private NavMeshAgent myNavMeshAgent;
	private PlayerHealth playerHealth;
	private Collider[] attackCollider;
	private Collider[] runCollider;
	private Collider [] walkCollider;
	private float attackRadius = 3;
	private float attackRate = 1.3f;
	private float runRadius = 30;
	private float walkRadius = 7;
	private float timer;

	void Awake () {
		anim = GetComponent<Animator>();
		aSource = GetComponent<AudioSource>();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth>();
		myNavMeshAgent = GetComponent<NavMeshAgent>();
	}

	void Update () {
		timer += Time.deltaTime;

		if (myNavMeshAgent.enabled == true) {
			anim.SetBool("isIdle", false);

			// Enemy detects player and runs towards it.
			runCollider = Physics.OverlapSphere(transform.position, runRadius, detectionLayer);

			if (runCollider.Length > 0) {
				myNavMeshAgent.SetDestination(runCollider[0].transform.position);
				anim.SetBool("isRunning", true);
				anim.SetBool("isIdle", false);
				anim.SetBool("isWalking", false);
				myNavMeshAgent.speed = 8;
			} 
			else {
				anim.SetBool("isRunning", false);
				anim.SetBool("isIdle", true);
			}

			// Enemy is closer to the player and walks towards the player.
			walkCollider = Physics.OverlapSphere(transform.position, walkRadius, detectionLayer);

			if(walkCollider.Length > 0) {
				anim.SetBool("isIdle", false);
				anim.SetBool("isRunning", false);
				anim.SetBool("isWalking", true);
				myNavMeshAgent.speed = 3;
			}

			if(timer >= attackRate){
				attackCollider = Physics.OverlapSphere(transform.position, attackRadius, detectionLayer);
				if(attackCollider.Length > 0) {
					timer = 0f;

					if (playerHealth.currentHealth > 0) {
						playerHealth.TakeDamage (attackDamage);
					}	

					myNavMeshAgent.SetDestination(gameObject.transform.position);

					anim.SetBool("isWalking", false);
					anim.SetBool("isRunning", false);
					anim.SetBool("isAttacking", true);
				} 
				else {
					anim.SetBool("isAttacking", false);
				}
			}
		}
	}
}
