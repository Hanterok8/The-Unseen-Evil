using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    public static string DemonNickName;

    private void Awake()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-4, 4), 3, Random.Range(-4, 4)), Quaternion.identity);
    }

}
