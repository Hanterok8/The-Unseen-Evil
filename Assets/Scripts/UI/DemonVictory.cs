using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class DemonVictory : MonoBehaviour
{
    [SerializeField] private GameObject victoryCanvas;
    private PhotonView _photonView;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        StartCoroutine(CheckNumberOfPlayers());
    }

    private IEnumerator CheckNumberOfPlayers()
    {
        yield return new WaitForSeconds(15);
        while (true)
        {
            yield return new WaitForSeconds(6);
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log(players.Length);
            if (players.Length == 1)
            {
                EndGame();
                break;
            }
        }
    }   
    private void EndGame()
    {   
        _photonView.RPC(nameof(EndGameRPC), RpcTarget.All);
        Invoke(nameof(MoveToLobby), 7);
    }
    [PunRPC]
    private void EndGameRPC()
    {
        victoryCanvas.SetActive(true);
    }
    private void MoveToLobby()
    {
        PhotonNetwork.CloseConnection(PhotonNetwork.LocalPlayer);
    }
}
