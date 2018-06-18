using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager { 

    public void MyStartHost() {
        StartHost();
        Debug.Log("Started Host");
    }

    public override void OnStartHost() {
        base.OnStartHost();
        Debug.Log("Host Started");
        FindObjectOfType<Camera>().gameObject.SetActive(false);
    }

    public override void OnStartClient(NetworkClient client) {
        base.OnStartClient(client);
        Debug.Log("Client start requested");
    }

    public override void OnClientConnect(NetworkConnection conn) {
        base.OnClientConnect(conn);
        Debug.Log("Client is connected.");
    }
}
