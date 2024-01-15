using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private GameObject StartButton;

    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerlistPrefab;

    private List<string> DemonNicknames = new List<string>();
    void Start()
    {
        Player player = PhotonNetwork.PlayerList[0];
        Instantiate(playerlistPrefab, playerListContent).GetComponent<PlayerListNickname>().SetUpNickname(player);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void StartTheGame()
    {
        StartButton.GetComponent<Button>().interactable = false;
        PlayerModeSet();
        for (int i = 0; i < DemonNicknames.Count; i++)
        {
            PlayerPrefs.SetString($"Demon{i}", DemonNicknames[i]);
            PlayerPrefs.Save();
        }
        PlayerPrefs.SetInt("PlayerCount", PhotonNetwork.CountOfPlayers);
        PlayerPrefs.Save();
        PhotonNetwork.LoadLevel("PlayLocation");
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
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);

    }
    public override void OnJoinedRoom()
    {
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        StartButton.SetActive(PhotonNetwork.IsMasterClient);
        ChangeSameNickNames();
        RecreatePlayerInRoomList();

    }
    private void ChangeSameNickNames()
    {
        string ourNickName;
        string otherPlayersNickName;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            ourNickName = PhotonNetwork.PlayerList[i].NickName;
            for (int j = 0; j < PhotonNetwork.PlayerList.Length; j++)
            {
                otherPlayersNickName = PhotonNetwork.PlayerList[j].NickName;
                if (ourNickName == otherPlayersNickName && i != j)
                {
                    PhotonNetwork.PlayerList[j].NickName += "1";
                }
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerlistPrefab, playerListContent).GetComponent<PlayerListNickname>().SetUpNickname(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Invoke(nameof(RecreatePlayerInRoomList), 0.2f);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public void PlayerModeSet()
    {
        int len = 2; // PhotonNetwork.CountOfPlayers / 3;
        if (len == 0) len = 1;
        System.Random rnd = new System.Random();
        int min = 0, max = PhotonNetwork.CountOfPlayers;
        int[] Demons = new int[len];
        Demons = Enumerable.Range(min, max).OrderBy(i => rnd.Next()).Take(len).ToArray();
        for (int i = 0; i < Demons.Length; i++)
        {
            DemonNicknames.Add(PhotonNetwork.PlayerList[Demons[i]].NickName);
        }
    }
}
