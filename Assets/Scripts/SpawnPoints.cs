using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public static SpawnPoints instance;

    public Transform spawnTeam1;
    public Transform spawnTeam2;
    public Transform flagSpawn;

    public void Awake()
    {
        if(transform.position == Vector3.zero)
        {
            instance= this;
        }
    }
    public void OnEnable()
    {
        instance.enabled = false;
        instance = this;
    }

    public Vector3 getSpawn(int team)
    {
        return team == 1 ? spawnTeam1.position : spawnTeam2.position;
    }
}
