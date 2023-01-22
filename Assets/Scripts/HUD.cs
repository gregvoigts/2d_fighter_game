using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(NetworkManager))]
public class HUD : MonoBehaviour
{
    [SerializeField] TMP_Text addressField;
    [SerializeField] NetworkManager manager;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Exit()
    {
        //TODO implement me
        Debug.Log("clicked exit");
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
}
