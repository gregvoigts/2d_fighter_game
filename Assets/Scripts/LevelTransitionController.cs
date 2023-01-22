using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionController : NetworkBehaviour
{
    public string SceneName = "Start";
    [SerializeField] int goalForTeam;

    void OnTriggerEnter2D(Collider2D col){
        if (!isServer) return;
        Player player;
        if(col.TryGetComponent<Player>(out player)) {
            if (player.hasFlag && player.team == goalForTeam)
            {
                SceneManager.LoadScene(SceneName);
            }
        }
    }
}
