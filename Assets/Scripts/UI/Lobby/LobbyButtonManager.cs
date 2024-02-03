using System;
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
    [SerializeField] private GameObject _playBg;

    [SerializeField] private Slider playerLimitValue;
    [SerializeField] private Slider sensetivity;
    [SerializeField] private Toggle visibleTogleButton;
    [SerializeField] private TMP_Text nickname;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text localRoomCode;

    LobbyManager lobbyManager;

    private void Start()
    => lobbyManager = FindFirstObjectByType<LobbyManager>();
    public void SetOrJoinRoom()
    {
        mainMenuButtons.SetActive(false);
        createOrJoinButtons.SetActive(true);
        _playBg.SetActive(true);
    }
    public void OnRoomNameChanged()
    {
        FixString(roomNameInputField);
        lobbyManager.createdRoomName = roomName.text;
    }
    private void FixString(TMP_InputField target)
    {
        target.lineType = TMP_InputField.LineType.MultiLineSubmit;
        target.textComponent.SetAllDirty();
        target.lineType = TMP_InputField.LineType.SingleLine;
    }
    public void OnLocalRoomCodeChanged()
    {
        lobbyManager.localRoomCode.text = localRoomCode.text;
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
        lobbyManager.maximumPlayers = (int)playerLimitValue.value;
    }
    public void VisibleStateChanged()
    {
        lobbyManager.isRoomVisible = visibleTogleButton.isOn;
    }
    public void OnEnteredNickname()
    {
        lobbyManager.playerNickname = nickname.text;
    }
    public void OnSensetivityChanged()
    {
        lobbyManager.sensetivityInGame = (int)sensetivity.value;
    }
}
