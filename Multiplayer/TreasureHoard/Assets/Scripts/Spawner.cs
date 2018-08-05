using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    public GameObject prefabToSpawn;

    [SerializeField]
    public int pooledAmount = 50;
    [SerializeField]
    private float minX, minZ = -1000f;
    [SerializeField]
    private float maxX, maxZ = 1000f;
    [SerializeField]
    private float borderOffset = 50f;

    private List<GameObject> objectPool = new List<GameObject>();

    public override void OnStartServer()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject go = Instantiate(prefabToSpawn, transform.position, Quaternion.identity, transform);
            NetworkServer.Spawn(go);
            go.SetActive(false);
            objectPool.Add(go);
        }
    }

    public void SpawnObject()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            int x = (int)Random.Range(minX + borderOffset, maxX - borderOffset);
            int z = (int)Random.Range(minZ + borderOffset, maxZ - borderOffset);

            if (!objectPool[i].activeInHierarchy)
            {
                objectPool[i].transform.position = new Vector3(x, 0, z);
                objectPool[i].transform.rotation = Quaternion.identity;
                objectPool[i].SetActive(true);
                NetworkServer.Spawn(objectPool[i]);
            }
        }
    }

    public void SetInActive(GameObject gameObject)
    {
        gameObject.SetActive(false);
        NetworkServer.UnSpawn(gameObject);
    }
}
