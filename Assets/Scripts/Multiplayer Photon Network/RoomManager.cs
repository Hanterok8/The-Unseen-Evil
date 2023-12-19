using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomName;
    void Start()
    {
        roomName.text = $"Room {PlayerPrefs.GetString("RoomName")}";
        PlayerPrefs.DeleteKey("RoomName");
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered the room", newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left the room", otherPlayer.NickName);
    }
}
