using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_Text LogText;
    [SerializeField] private int maximumPlayers;
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
        int num = Random.Range(0, 25);
        char let = (char)('a' + num);
        char upperChar = char.ToUpper(let);
        return upperChar.ToString();
    }

    public void CreateRoom()
=>      PhotonNetwork.CreateRoom(RandomLetter(), new Photon.Realtime.RoomOptions { MaxPlayers = maximumPlayers });
    public void JoinRoom()
=>      PhotonNetwork.JoinRandomRoom();

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
