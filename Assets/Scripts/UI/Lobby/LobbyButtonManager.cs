using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject createOrJoinButtons;
    [SerializeField] private GameObject settingRoom;
    [SerializeField] private GameObject joinByCode;

    [SerializeField] private Slider playerLimitValue;
    [SerializeField] private Slider sensitivity;
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
        ButtonScale buttonScale = GetComponent<ButtonScale>();
        if (buttonScale.isClicked) return;
        createOrJoinButtons.SetActive(true);
        buttonScale.MiddleButton();
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
        createOrJoinButtons.GetComponent<Animator>().SetTrigger("Close");
        settingRoom.SetActive(true);
    }

    public void BackFromSettingRoom()
    {
        createOrJoinButtons.SetActive(true);
        settingRoom.GetComponent<Animator>().SetTrigger("Close");
    }

    public void BackFromJoinByCode()
    {
        createOrJoinButtons.SetActive(true);
        joinByCode.GetComponent<Animator>().SetTrigger("Close");
    }
    public void JoinRoom()
    {
        createOrJoinButtons.GetComponent<Animator>().SetTrigger("Close");
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
    public void OnSensitivityChanged()
    {
        lobbyManager.sensitivityInGame = (int)sensitivity.value;
    }
}
