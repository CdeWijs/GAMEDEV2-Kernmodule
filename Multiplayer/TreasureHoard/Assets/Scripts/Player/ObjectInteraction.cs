using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectInteraction : NetworkBehaviour {

    private Player player;
    private Spawner spawner;
    private int scoreAmount = 10;

    private void Start() {
        player = GetComponent<Player>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "TreasureChest") {
            CmdPickup(other.gameObject);
            player.AddScore(scoreAmount);
        }
    }

    [Command]
    private void CmdPickup(GameObject _object) {
        RpcPickup(_object);
    }

    [ClientRpc]
    private void RpcPickup(GameObject _object) {
        spawner.SetInActive(_object);
        spawner.Spawn();
    }
}
