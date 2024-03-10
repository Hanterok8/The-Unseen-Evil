using System.Collections;
using Photon.Pun;
using Photon.Realtime;
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
    [SerializeField] private GameObject loadingScreen;
    private PhotonView photonView;

    private string DemonPlayer;

    void Start()
    {
        Player player = PhotonNetwork.PlayerList[0];
        photonView = GetComponent<PhotonView>();
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
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        DemonChooser();
        photonView.RPC(nameof(LoadScene), RpcTarget.All);
        PhotonNetwork.LoadLevel("PlayLocation");
    }
    [PunRPC]
    private void LoadScene()
    {
        loadingScreen.SetActive(true);
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
    private void DemonChooser()
    {
        int demonIndex = Random.Range(0, PhotonNetwork.PlayerList.Length);
        DemonPlayer = PhotonNetwork.PlayerList[demonIndex].NickName;
        Debug.LogError(photonView.Owner.NickName + " " + DemonPlayer);
        photonView.RPC(nameof(PlayerModeSetRPC), RpcTarget.All, DemonPlayer);
    }
    [PunRPC]
    private void PlayerModeSetRPC(string nickname)
    {
        DemonSaver.UpdateDemonName(nickname);
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
                    ChangeSameNickNames();
                    return;
                }
            }
        }
    }
}
