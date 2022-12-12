using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Body : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SquatStart(float squatHigh)
    {
        transform.localScale += Vector3.down* squatHigh * 2;
        transform.localPosition -= Vector3.down* squatHigh;
    }

    public void SquatEnd(float squatHigh)
    {
        transform.localScale+= Vector3.up* squatHigh *2;
        transform.localPosition -= Vector3.up * squatHigh;
    }
}
