using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text localRoomCode;

    [SerializeField] private TMP_Text LogText;
    public int maximumPlayers;
    public string playerNickname;
    public bool isRoomVisible = true;
    void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);
        Log("Player`s name is set to " + PhotonNetwork.NickName);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
=>      Log("Connected to master");

    private string RandomLetter()
    {
        System.Random rnd = new System.Random();
        char randomChar = (char)rnd.Next('a', 'z');
        char upperChar = char.ToUpper(randomChar);
        return upperChar.ToString();
    }

    public void CreateRoom()
    {
        if(playerNickname != "player nickname is not entered")
            PhotonNetwork.NickName = playerNickname;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maximumPlayers;
        roomOptions.IsVisible = isRoomVisible;
        string randomLetter = RandomLetter();
        Log("Trying to join room " + randomLetter);
        PhotonNetwork.CreateRoom(randomLetter, roomOptions);

    }
    public void JoinPublicRoom()
    {
        if (playerNickname != "player nickname is not entered")
            PhotonNetwork.NickName = playerNickname;
        PhotonNetwork.JoinRandomRoom();
    }
    public void JoinLocalRoom()
    {
        if (playerNickname != "player nickname is not entered")
            PhotonNetwork.NickName = playerNickname;
        try
        {
            PhotonNetwork.JoinRoom(localRoomCode.text);
        }
        catch
        {
            Log("This room does not exist");
        }
    }
    public override void OnJoinedRoom()
    {
        Log("Joined the room");

        PhotonNetwork.LoadLevel("Matchmaking");
    }

    

    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}
