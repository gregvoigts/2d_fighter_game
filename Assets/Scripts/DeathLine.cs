using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLine : NetworkBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collision");
        if (!isServer)
        {
            return;
        }
        Player player;
        if(col.gameObject.TryGetComponent<Player>(out player)){
            player.hit(9999);
        }
    }
}
