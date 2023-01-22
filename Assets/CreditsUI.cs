using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsUI : MonoBehaviour
{
    public float speed = 9f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.up + Vector3.forward) * Time.deltaTime * speed;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Home();
        }
    }

    public void Home()
    {
        SceneManager.LoadScene("Offline");
    }

    public void Replay()
    {
        GetComponent<RectTransform>().localPosition = new Vector3(0, -350, -200);        
    }
}
