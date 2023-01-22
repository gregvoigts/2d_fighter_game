using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionController : NetworkBehaviour
{
    [SerializeField] SpawnPoints spawnPoints;
    [SerializeField] int goalForTeam;

    void OnTriggerEnter2D(Collider2D col){
        if (!isServer) return;
        Player player;
        if(col.TryGetComponent<Player>(out player)) {
            if (player.hasFlag && player.team == goalForTeam)
            {
                spawnPoints.gameObject.SetActive(true);
                switchMap();
            }
        }
    }

    [ClientRpc]
    void switchMap()
    {
        spawnPoints.gameObject.SetActive(true);
        Player._players.ForEach(p => p.changeLocation(spawnPoints.getSpawn(p.team)));
    }
}
