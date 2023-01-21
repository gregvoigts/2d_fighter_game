using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlagController : NetworkBehaviour
{
    public static FlagController instance { get; private set; }

    private void Awake()
    {
        Debug.Log("Flag awaked");
        instance= this;
        if (NetworkServer.active)
        {
            NetworkServer.Spawn(this.gameObject);
        }
    }

    private void Start()
    {
        Debug.Log(this.isActiveAndEnabled);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(!isServer)
        {
            return;
        }
        Player player;
       
        if (col.TryGetComponent<Player>(out player) && player.deathTimer == 0)
        {
            print("Flag picked up by:" + col.name);
            player.hasFlag= true;
            SetActive(false);
        }
    }

    [Server]
    public void SetActive(bool value)
    {
        _clientSetActive(value);
        gameObject.SetActive(value);
    }

    [ClientRpc]
    void _clientSetActive(bool value) { gameObject.SetActive(value);}
}
