using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    public string[] DemonNicknames = new string[2];
    public GameObject spawnedPlayer;
    private List<PlayerNickName> playersNames = new List<PlayerNickName>();
    void Awake()
    {
        spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-4, 4), 3, Random.Range(-4, 4)), Quaternion.identity);

    }
    private void Update()
    {
        if (PlayerPrefs.HasKey("PlayersInLobby"))
        {
            Time.timeScale = 0;
            Debug.Log(PhotonNetwork.CountOfPlayers + "- players, needed players - " + PlayerPrefs.GetInt("PlayersInLobby"));
            if (PhotonNetwork.CountOfPlayers == PlayerPrefs.GetInt("PlayersInLobby"))
            {
                MakePlayersDemons();
                Time.timeScale = 1;
                PlayerPrefs.DeleteKey("PlayersInLobby");
            }
            
        }
    }


    public void PlayerModeSet()
    {
        int len = PhotonNetwork.CountOfPlayers / 3;
        if (len == 0) len = 1;
        System.Random rnd = new System.Random();
        int min = 0, max = PhotonNetwork.CountOfPlayers;
        int[] Demons = new int[len];
        Demons = Enumerable.Range(min, max).OrderBy(i => rnd.Next()).Take(len).ToArray();
        for (int i = 0; i < Demons.Length; i++)
        {
            DemonNicknames[i] = PhotonNetwork.PlayerList[Demons[i]].NickName;
        }
    }
    private void MakePlayersDemons()
    {
        PlayerModeSet();
        PlayerNickName[] playersNames = Object.FindObjectsOfType<PlayerNickName>();
        Debug.Log(playersNames.Length + " players");
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            for (int j = 0; j < DemonNicknames.Length; j++)
            {
                if (playersNames[i].nickName == DemonNicknames[j])
                {
                    playersNames[i].GetComponent<PlayerOrDemon>().isDemon = true;
                }
            }

        }
    }

}
