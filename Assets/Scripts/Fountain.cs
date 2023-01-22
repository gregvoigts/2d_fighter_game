using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    private bool active = false;
    private GameObject[] fountains;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Controller());
        fountains = GameObject.FindGameObjectsWithTag("Fountain");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject == null) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            if (active)
            {
                // kill player
                Debug.Log("KILL");
            }
        }
    }

    public void Enable()
    {
        foreach(GameObject obj in fountains)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        active = true;
    }

    public void Disable()
    {
        active = false;
        foreach(GameObject obj in fountains)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    IEnumerator Controller()
    {
        while (true)
        {
            yield return new WaitForSeconds(7);
            Enable();
            yield return new WaitForSeconds(3);
            Disable();
        }
    }
}
