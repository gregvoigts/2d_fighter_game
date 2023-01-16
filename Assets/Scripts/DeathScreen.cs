using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{

    public static DeathScreen instance;
    DeathCounter deathCounter;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        deathCounter= GetComponentInChildren<DeathCounter>();
        gameObject.SetActive(false);
    }

    public void UpdateDeathCounter(float counter)
    {
        if(counter> 0)
        {
            gameObject.SetActive(true);
            deathCounter.UpdateCounter(counter);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
