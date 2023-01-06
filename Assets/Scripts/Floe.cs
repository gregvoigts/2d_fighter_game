using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floe : MonoBehaviour
{
    private int OnFloe = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Sink
            OnFloe++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Rise
            OnFloe--;
        }
    }
}
