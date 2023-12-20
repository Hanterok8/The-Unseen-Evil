using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject createOrJoinButtons;
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject settingRoom;
    [SerializeField] private GameObject joinByCode;

    [SerializeField] private Slider playerLimitValue;
    [SerializeField] private Toggle visibleTogleButton;
    [SerializeField] private TMP_Text nickname;
    public void SetOrJoinRoom()
    {
        mainMenuButtons.SetActive(false);
        createOrJoinButtons.SetActive(true);
    }
    public void SetRoom()
    {
        createOrJoinButtons.SetActive(false);
        settingRoom.SetActive(true);
    }
    public void JoinRoom()
    {
        createOrJoinButtons.SetActive(false);
        joinByCode.SetActive(true);
    }


    public void MaxNumberOfPlayersChanged()
    {
        LobbyManager lobbyManager = Object.FindFirstObjectByType<LobbyManager>();
        lobbyManager.maximumPlayers = (int)playerLimitValue.value;
    }
    public void VisibleStateChanged()
    {
        LobbyManager lobbyManager = Object.FindFirstObjectByType<LobbyManager>();
        lobbyManager.isRoomVisible = visibleTogleButton.isOn;
    }
    public void OnEnteredNickname()
    {
        LobbyManager lobbyManager = Object.FindFirstObjectByType<LobbyManager>();
        lobbyManager.playerNickname = nickname.text;
    }
}
