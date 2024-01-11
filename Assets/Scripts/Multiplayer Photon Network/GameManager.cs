using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;

    public GameObject spawnedPlayer;
    //private PhotonView photonView;
    private List<string> DemonNicknames = new List<string>();

    void Awake()
    {
        int numberOfDemonPlayers = PlayerPrefs.GetInt("PlayerCount") / 3;
        if (numberOfDemonPlayers == 0) numberOfDemonPlayers++;
        for (int i = 0; i < numberOfDemonPlayers; i++)
        {
            Debug.Log(PlayerPrefs.GetString($"Demon{i}"));
            //DemonNicknames[i] = PlayerPrefs.GetString($"Demon{i}");
            DemonNicknames.Add(PlayerPrefs.GetString($"Demon{i}"));
        }
        //photonView = GetComponent<PhotonView>();
        spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-4, 4), 3, Random.Range(-4, 4)), Quaternion.identity);
        Invoke(nameof(MakePlayersDemons), 0.1f);

    }
    private void Update()
    {
        //if (PlayerPrefs.HasKey("PlayersInLobby"))
        //{
        //    photonView.RPC(nameof(SetTimeScale), RpcTarget.All, 0);
        //    Debug.Log(PhotonNetwork.CountOfPlayers + "- players, needed players - " + PlayerPrefs.GetInt("PlayersInLobby"));
        //    if (PhotonNetwork.CountOfPlayers == PlayerPrefs.GetInt("PlayersInLobby"))
        //    {
        //        photonView.RPC(nameof(SetTimeScale), RpcTarget.All, 1);
        //        PlayerPrefs.DeleteKey("PlayersInLobby");
        //    }

        //}
    }
    //[PunRPC]
    //private void SetTimeScale(int newTimeScale)
    //{
    //    Time.timeScale = newTimeScale;
    //}


    //public void PlayerModeSet()
    //{
    //    int len = PhotonNetwork.CountOfPlayers / 3;
    //    if (len == 0) len = 1;
    //    System.Random rnd = new System.Random();
    //    int min = 0, max = PhotonNetwork.CountOfPlayers;
    //    int[] Demons = new int[len];
    //    Demons = Enumerable.Range(min, max).OrderBy(i => rnd.Next()).Take(len).ToArray();
    //    for (int i = 0; i < Demons.Length; i++)
    //    {
    //        DemonNicknames[i] = PhotonNetwork.PlayerList[Demons[i]].NickName;
    //    }
    //}
    private void MakePlayersDemons()
    {
        //PlayerModeSet();
        PlayerNickName[] playersNames = FindObjectsOfType<PlayerNickName>();
        Debug.Log(playersNames.Length + " players");
        for (int i = 0; i < PhotonNetwork.CountOfPlayers; i++)
        {
            for (int j = 0; j < DemonNicknames.Count; j++)
            {
                if (playersNames[i].nickName == DemonNicknames[j])
                {
                    playersNames[i].GetComponent<PlayerOrDemon>().isDemon = true;
                }
            }

        }
    }

}
