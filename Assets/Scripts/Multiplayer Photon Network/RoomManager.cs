using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomName;

    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerlistPrefab;
    void Start()
    {
        Player player = PhotonNetwork.PlayerList[0];
        Instantiate(playerlistPrefab, playerListContent).GetComponent<PlayerListNickname>().SetUpNickname(player);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);

    }
    public override void OnJoinedRoom()
    {
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        RecreatePlayerInRoomList();

    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerlistPrefab, playerListContent).GetComponent<PlayerListNickname>().SetUpNickname(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Invoke(nameof(RecreatePlayerInRoomList), 0.2f);
    }
    private void RecreatePlayerInRoomList()
    {
        Player[] playersInRoom = PhotonNetwork.PlayerList;

        foreach (Transform transf in playerListContent)
        {
            Destroy(transf.gameObject);
        }
        for (int i = 0; i < playersInRoom.Length; i++)
        {
            Instantiate(playerlistPrefab, playerListContent).GetComponent<PlayerListNickname>().SetUpNickname(playersInRoom[i]);
        }
    }
}
