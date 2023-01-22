using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [SerializeField] TMP_Text addressField;
    NetworkManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("RoomManager").GetComponent<NetworkManager>();
    }

    public void Exit()
    {
        Debug.Log("Attempt to close application...");
        Application.Quit();
    }

    public void Connect()
    {           
        manager.StartClient();
    }

    public void Host()
    {
        Debug.Log("clicked host");
        manager.StartHost();
    }

    public void valueChange(string value)
    {
        Debug.Log($"clicked connect: {value}");
        manager.networkAddress = value;
    }

    public void showCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
