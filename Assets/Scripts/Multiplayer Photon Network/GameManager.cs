using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    void Awake()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-2, 2),0, Random.Range(-2, 2)), Quaternion.identity);
    }
    void Update()
    {
        
    }
}
