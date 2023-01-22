using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosed : MonoBehaviour
{
    private GameObject doorOpen;
    private GameObject doorClosed;
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = GameObject.FindGameObjectWithTag("DoorOpen");
        doorClosed = GameObject.FindGameObjectWithTag("DoorClosed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        doorClosed.SetActive(true);
        doorOpen.SetActive(false);
    }
}
