using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    NetworkManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("RoomManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onQuit()
    {
        // stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            manager.StopHost();
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            manager.StopClient();
        }
    }
}
