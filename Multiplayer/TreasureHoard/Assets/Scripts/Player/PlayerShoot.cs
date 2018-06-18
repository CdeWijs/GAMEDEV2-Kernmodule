using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    public int damage = 10;
    public float range = 200f;
    
    [SerializeField]
    private LayerMask mask;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    [Client]
    private void Shoot() {
        RaycastHit _hit;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out _hit, range, mask)) {
            Debug.Log(_hit.collider.name);
            if (_hit.collider.tag == "Player") {
                CmdPlayerShot(_hit.collider.name, damage);
            }
        }
    }

    [Command]
    private void CmdPlayerShot(string _playerID, int _damage) {
        Debug.Log(_playerID + " has been shot.");
        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);
    }
}
