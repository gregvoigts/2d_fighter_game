using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    private BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        print(col.name + " took the flag!");
        transform.SetParent(col.transform);
        transform.localPosition = new Vector3(-.5f, 0.5f, 1);
    
        collider.enabled = false;
    }
}
