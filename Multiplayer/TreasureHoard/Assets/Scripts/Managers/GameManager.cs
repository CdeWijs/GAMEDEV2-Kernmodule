using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    public static Dictionary<string, Player> players = new Dictionary<string, Player>();

    private const string PLAYER_ID_PREFIX = "Player ";
    private bool startGame = false;
    private Spawner spawnerScript;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        spawnerScript = FindObjectOfType<Spawner>();
    }

    private void Update()
    {
        if (players.Count == 2 && startGame == false)
        {
            Debug.Log("Two players have joined the game.");
            startGame = true;
            Player randPlayer = GetRandomPlayer();
            if (randPlayer)
            {
                randPlayer.MyTurn = true;
                Debug.Log(randPlayer + "'s turn is true");
            }
            for (int i = 0; i < 50; i++)
            {
                spawnerScript.SpawnObject();
            }
            foreach (KeyValuePair<string, Player> player in players)
            {
                player.Value.playerUI.ShowWaitingText(false);
            }
        } 
        else if (players.Count < 2)
        {
            foreach (KeyValuePair<string, Player> player in players)
            {
                player.Value.playerUI.ShowWaitingText(true);
            }
        }
    }

    private Player GetRandomPlayer()
    {
        int rand = UnityEngine.Random.Range(0, 1);
        int index = 0;
        foreach (KeyValuePair<string, Player> player in players)
        {
            index++;
            if (index != rand)
            {
                return player.Value;
            }
        }
        return null;
    }
    
    public static void SwitchTurns()
    {
        foreach (KeyValuePair<string, Player> player in players)
        {
            if (player.Value.MyTurn == false)
            {
                player.Value.MyTurn = true;
                Debug.Log(player.Key + "'s turn is now true.");
            }
            else
            {
                player.Value.MyTurn = false;
                Debug.Log(player.Key + "'s turn is now false.");
            }
        }
    }

    public static void RegisterPlayer(string netID, Player player)
    {
        string playerID = PLAYER_ID_PREFIX + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static void UnregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    public static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }
}
