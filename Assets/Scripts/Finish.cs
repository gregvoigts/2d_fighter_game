using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : NetworkBehaviour
{

    [SerializeField] Canvas endScreen;
    [SerializeField] int goalForTeam;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isServer) return;
        Debug.Log("entered Finish");
        Player player;
        if (col.TryGetComponent<Player>(out player))
        {
            if (player.hasFlag && player.team == goalForTeam)
            {
                showEndScreen();
            }
        }
    }

    [ClientRpc]
    void showEndScreen()
    {
        endScreen.gameObject.SetActive(true);
    }
}
