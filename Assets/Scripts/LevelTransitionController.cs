using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionController : MonoBehaviour
{
    public bool IsNextLevel = false;
    public string SceneName = "Lobby";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        SceneManager.LoadScene(SceneName);
    }
}
