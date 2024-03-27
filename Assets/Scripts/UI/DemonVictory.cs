using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
        while (true)
        {
            yield return new WaitForSeconds(6);
            GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerInstance");
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
    }
    [PunRPC]
    private void EndGameRPC()
    {
        victoryCanvas.SetActive(true);
    }
}
