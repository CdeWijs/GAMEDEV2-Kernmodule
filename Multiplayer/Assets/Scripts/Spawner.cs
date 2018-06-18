using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour {

    public GameObject prefab;
    public int pooledAmount = 10;
    public float minX, maxX, minZ, maxZ;
    public float offset = 50f;

    private List<GameObject> objectPool = new List<GameObject>();

    public override void OnStartServer() {
        for (int i = 0; i < pooledAmount; i++) {
            GameObject _go = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            NetworkServer.Spawn(_go);
            _go.SetActive(false);
            objectPool.Add(_go);
        }

        for (int i = 0; i < 10; i++) {
            Spawn();
        }
    }

    public void Spawn() {
        for (int i = 0; i < objectPool.Count; i++) {

            // Get random values between minX, maxY, minY and maxY.
            // offset makes the chance that candies spawn outside of the border smaller.
            int x = (int)Random.Range(minX + offset, maxX - offset);
            int z = (int)Random.Range(minZ + offset, maxZ - offset);

            if (!objectPool[i].activeInHierarchy) {
                objectPool[i].transform.position = new Vector3(x, 0, z);
                objectPool[i].transform.rotation = Quaternion.identity;
                objectPool[i].SetActive(true);
                NetworkServer.Spawn(objectPool[i]);
            }
        }
    }

    public void SetInActive(GameObject _object) {
        _object.SetActive(false);
        NetworkServer.UnSpawn(_object);
    }
}
