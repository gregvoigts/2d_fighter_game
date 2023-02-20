using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    private bool active;
    
    // Start is called before the first frame update
    void Start()
    {
        Disable();
        StartCoroutine(Controller());
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
                Player player;
                if (collision.gameObject.TryGetComponent<Player>(out player))
                {
                    player.hit(9999);
                }
            }
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        active = true;
    }

    public void Disable()
    {
        active = false;
        gameObject.SetActive(false);
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
