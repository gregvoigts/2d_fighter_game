using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(NetworkManager))]
public class HUD : MonoBehaviour
{
    [SerializeField] TMP_Text addressField;
    NetworkManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInChildren<NetworkManager>();        
    }

    public void Exit()
    {
        //TODO implement me
        Debug.Log("clicked exit");
    }

    public void Connect()
    {
        Debug.Log($"clicked connect: {addressField.text}");
        manager.networkAddress = addressField.text;
        manager.StartClient();
    }

    public void Host()
    {
        Debug.Log("clicked host");
        manager.StartHost();
    }
}
