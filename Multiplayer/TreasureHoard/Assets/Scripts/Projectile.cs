using System;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;
    [SerializeField]
    private int damage = 10;
    private ParticleSystem particles;
    private GameObject particleEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            particleEffect = Instantiate(particlePrefab);
            particleEffect.transform.position = transform.position;
            particles = particleEffect.GetComponent<ParticleSystem>();
            if (hasAuthority)
            {
                CmdSpawnSplash();
            }
        }

        if (other.tag == "Player")
        {
            string playerID = other.name;
            Debug.Log(playerID + " has been shot.");
            Player _player = GameManager.GetPlayer(playerID);
            _player.RpcTakeDamage(damage);
            Destroy(gameObject);
        }
    }

    [Command]
    private void CmdSpawnSplash()
    {
        NetworkServer.Spawn(particleEffect);
    }

    private void Update()
    {
        if (particles && particles.IsAlive() == false)
        {
            Destroy(particleEffect);
            NetworkServer.UnSpawn(particleEffect);
            Destroy(gameObject);
        }
    }
}
