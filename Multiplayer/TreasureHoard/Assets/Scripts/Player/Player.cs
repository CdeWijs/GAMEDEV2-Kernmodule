using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    // Members
    public bool MyTurn { get; set; }

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private float startHealth = 100f;
    [SyncVar]
    private float currentHealth;
    [SyncVar]
    private int score;
    private Vector3 startPosition;

    // References
    public PlayerUI playerUI;
    private PlayerShoot playerShoot;

    private void Start()
    {
        MyTurn = false;
        if (isLocalPlayer)
        {
            startPosition = transform.position;
        }
        playerShoot = GetComponent<PlayerShoot>();
        playerUI = GetComponent<PlayerUI>();
        ResetPlayer();
    }

    private void Update()
    {
        if (MyTurn)
        {
            SetShootActive(true);
        }
        else
        {
            SetShootActive(false);
        }
    }

    public void ResetPlayer()
    {
        currentHealth = startHealth;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = true;
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetScore()
    {
        return score;
    }

    [ClientRpc]
    public void RpcAddScore(int _amount)
    {
        score += _amount;
        playerUI.UpdateScore();
    }

    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        currentHealth -= amount;
        playerUI.UpdateHealth();
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
            StartCoroutine(playerUI.ShowWreckedText());
        }
    }

    private IEnumerator Die()
    {
        Debug.Log(gameObject.name + " has died.");
        StartCoroutine(ScoreManager.PostScores(score));
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        yield return new WaitForSeconds(5);
        if (isLocalPlayer)
        {
            transform.position = startPosition;
            ResetPlayer();
            playerUI.UpdateHealth();
        }
        yield return null;
    }

    public void SetShootActive(bool isActive)
    {
        playerShoot.enabled = isActive;
    }
}