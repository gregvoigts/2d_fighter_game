using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayerUI : MonoBehaviour
{
    [Header("Player panel")]
    [SerializeField] Sprite onReadyImage;
    [SerializeField] Sprite onNotReadyImage;
    [SerializeField] Image readyImage;
    [SerializeField] TMP_Text playerNameText;

    [Header("Ready button")]
    [SerializeField] Sprite buttonOnReadyImage;
    [SerializeField] Sprite buttonOnNotReadyImage;
    [SerializeField] string buttonOnReadyText;
    [SerializeField] string buttonOnNotReadyText;
    Image readyButtonImage;
    TMP_Text readyButtonText;

    GameObject readyButton;
    bool isLocalPlayer = false;

    NetworkRoomPlayer player;

    private void Start()
    {

    }
    public void initReadyButton()
    {
        readyButton = GameObject.Find("ReadyButton");
        readyButtonImage = readyButton.GetComponent<Image>();
        readyButtonText = readyButton.GetComponentInChildren<TMP_Text>();

        readyButtonText.text = buttonOnNotReadyText;
        readyButtonImage.sprite = buttonOnNotReadyImage;

        readyButton.GetComponent<Button>().onClick.AddListener(onClick);
    }

    public void onClick()
    {
        Debug.Log("Button was clicked");
        player.CmdChangeReadyState(!player.readyToBegin);
    }

    // Sets a highlight color for the local player
    public void SetLocalPlayer(NetworkRoomPlayer player)
    {
        this.player = player;
        initReadyButton();
        isLocalPlayer = true;
        playerNameText.text = string.Format("YOU");

    }

    // This value can change as clients leave and join
    public void OnPlayerNumberChanged(byte newPlayerNumber)
    {
        playerNameText.text = string.Format("Player {0:00}", newPlayerNumber);
    }    
    public void OnReadyStateChanged(bool newState)
    {
        readyImage.sprite = newState ? onReadyImage : onNotReadyImage;

        if (!isLocalPlayer) return;
        readyButtonImage.sprite = newState ? buttonOnReadyImage : buttonOnNotReadyImage;
        readyButtonText.text = newState ? buttonOnReadyText : buttonOnNotReadyText;
    }
}
