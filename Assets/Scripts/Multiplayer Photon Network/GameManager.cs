using Photon.Pun;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    public string[] DemonNicknames = new string[2];
    void Awake()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-4, 4),3, Random.Range(-4, 4)), Quaternion.identity);
        MakePlayersDemons();
    }


    public void PlayerModeSet()
    {
        int len = 1;
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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = 0; j < DemonNicknames.Length; j++)
            {
                Transform playersParent = players[i].transform.parent;
                if (playersParent.GetComponent<PlayerNickName>().nickName == DemonNicknames[j])
                {
                    playersParent.GetComponent<PlayerOrDemon>().isDemon = true;
                }
            }

        }
    }

}
