using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionController : MonoBehaviour
{
    public bool IsNextLevel = false;
    public string SceneName = "Start";

    void OnTriggerEnter2D(Collider2D col){
        if(col.transform.Find("Flag")?.gameObject?.activeSelf ?? false) {
            SceneManager.LoadScene(SceneName);
        }
    }
}
