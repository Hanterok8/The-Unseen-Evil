using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;

    public GameObject spawnedPlayer;
    private List<string> DemonNicknames = new List<string>();
    private void Awake()
    {
        spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-4, 4), 3, Random.Range(-4, 4)), Quaternion.identity);
    }

}
