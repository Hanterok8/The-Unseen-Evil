using System.Collections;
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

    private LobbyManager lobbyManager;
    [SerializeField] private int backButtonInSettingRoomCooldown;
    [SerializeField] private int backButtonInLocalRoomCooldown;

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
        Animator animator = createOrJoinButtons.GetComponent<Animator>();
        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");
        settingRoom.SetActive(true);
    }

    public void BackFromSettingRoom()
    {
        Debug.Log(backButtonInSettingRoomCooldown.ToString() + " before if");
        if (backButtonInSettingRoomCooldown > 0) return;
        backButtonInSettingRoomCooldown = 5;
        createOrJoinButtons.SetActive(true);
        Animator animator = settingRoom.GetComponent<Animator>();
        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");
        StopCoroutine(UpdateCooldownSettingRoom());
        StartCoroutine(UpdateCooldownSettingRoom());
    }

    public void BackFromJoinByCode()
    {
        if (backButtonInLocalRoomCooldown > 0) return;
        backButtonInLocalRoomCooldown = 5;
        createOrJoinButtons.SetActive(true);
        joinByCode.GetComponent<Animator>().SetTrigger("Close");
        StopCoroutine(UpdateCooldownLocalRoom());
        StartCoroutine(UpdateCooldownLocalRoom());
    }

    private IEnumerator UpdateCooldownSettingRoom()
    {
        while (backButtonInSettingRoomCooldown > 0)
        {
            yield return new WaitForSeconds(1);
            backButtonInSettingRoomCooldown--;
            Debug.Log(backButtonInSettingRoomCooldown.ToString() + " in coroutine");
        }
    }
    private IEnumerator UpdateCooldownLocalRoom()
    {
        while (backButtonInLocalRoomCooldown > 0)
        {
            yield return new WaitForSeconds(1);
            backButtonInLocalRoomCooldown--;
        }
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
        if (IsEmpty(nickname.text))
        {
            lobbyManager.playerNickname = "player nickname is not entered";
            return;
        }
        lobbyManager.playerNickname = nickname.text;
    }
    public void OnSensitivityChanged()
    {
        lobbyManager.sensitivityInGame = (int)sensitivity.value;
    }
    private bool IsEmpty(string message)
    {
        if (string.IsNullOrEmpty(message)) return true;
        else
        {
            char[] messageInChar = message.ToCharArray();
            foreach (char symbol in messageInChar)
            {
                if (symbol != ' ')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
