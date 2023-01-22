using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DoorOpen : MonoBehaviour
{
    private GameObject doorOpen;
    private GameObject doorClosed;
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = GameObject.FindGameObjectWithTag("DoorOpen");
        doorOpen.SetActive(false);
        doorClosed = GameObject.FindGameObjectWithTag("DoorClosed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            doorOpen.SetActive(false);
            doorClosed.SetActive(true);
        }
    }
}
