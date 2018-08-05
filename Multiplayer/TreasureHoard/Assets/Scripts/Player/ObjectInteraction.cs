using UnityEngine;
using UnityEngine.Networking;

public class ObjectInteraction : NetworkBehaviour
{
    [SerializeField]
    private int scoreAmount = 10;
    private Spawner objectSpawner;

    private void Start()
    {
        objectSpawner = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = GameManager.GetPlayer(other.name);
            player.RpcAddScore(scoreAmount);
            CmdPickup();
        }
    }

    [Command]
    private void CmdPickup()
    {
        Debug.Log("Rpc " + gameObject);
        objectSpawner.SetInActive(gameObject);
        objectSpawner.SpawnObject();
    }
}
