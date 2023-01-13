using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlagController : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col){
        var flag = col.transform.Find("Flag")?.gameObject;
        if (flag)
        {
            print("Flag picked up by:" + col.name);
            flag.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
