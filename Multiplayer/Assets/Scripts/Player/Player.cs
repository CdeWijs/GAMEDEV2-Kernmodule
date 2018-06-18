using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] disableOnDeath;

    private float startHealth = 100f;
    [SyncVar]
    private float currentHealth;
    [SyncVar]
    private bool isDead = false;
    [SyncVar]
    private int score;

    private PlayerUI playerUI;

    private void Start() {
        ResetPlayer();
        playerUI = GetComponent<PlayerUI>();
    }

    public void ResetPlayer() {
        currentHealth = startHealth;
        isDead = false;

        for (int i = 0; i < disableOnDeath.Length; i++) {
            disableOnDeath[i].enabled = true;
        }
    }

    public float GetCurrentHealth() {
        return currentHealth;
    }

    public float GetScore() {
        return score;
    }

    public void AddScore(int _amount) {
        score += _amount;
        playerUI.UpdateScore();
    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount) {
        currentHealth -= _amount;
        playerUI.UpdateHealth();
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        isDead = true;
        StartCoroutine(ScoreManager.PostScores(score));

        for (int i = 0; i < disableOnDeath.Length; i++) {
            disableOnDeath[i].enabled = false;
        }

        ResetPlayer();
    }
}
