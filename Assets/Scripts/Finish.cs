using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{

    [SerializeField] Canvas endScreen;
    [SerializeField] int goalForTeam;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("entered Finish");
        Player player;
        if (col.TryGetComponent<Player>(out player))
        {
            if (player.hasFlag && player.team == goalForTeam)
            {
                endScreen.gameObject.SetActive(true);
            }
        }
    }
}
