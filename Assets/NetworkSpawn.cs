using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        NetworkServer.SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
