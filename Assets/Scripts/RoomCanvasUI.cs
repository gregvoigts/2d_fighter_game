using Mirror;
using UnityEngine;

public class RoomCanvasUI : MonoBehaviour
{
    [Tooltip("Assign Main Panel so it can be turned on from Player:OnStartClient")]
    public RectTransform mainPanel;

    [Tooltip("Assign Players Panel for instantiating PlayerUI as child")]
    public RectTransform playersPanel;

    // static instance that can be referenced from static methods below.
    static RoomCanvasUI instance;

    NetworkManager manager;

    void Awake()
    {
        instance = this;
        manager = GameObject.Find("RoomManager").GetComponent<NetworkManager>();
    }

    public static void SetActive(bool active)
    {
        instance.mainPanel.gameObject.SetActive(active);
    }

    public static RectTransform GetPlayersPanel() => instance.playersPanel;

    public void BackToLobby()
    {
        // stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            manager.StopHost();
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            manager.StopClient();
        }
    }
}