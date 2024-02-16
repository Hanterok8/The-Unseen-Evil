using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReceiver : MonoBehaviour
{
    private const int DISTANCE = 2;

    [SerializeField] private GameObject model;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Camera _camera;

    private ItemControl itemController;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        itemController = GetPlayerItemController();
    }

    private ItemControl GetPlayerItemController()
    {
        CurrentPlayer[] currentPlayers = FindObjectsOfType<CurrentPlayer>();
        foreach (CurrentPlayer player in currentPlayers)
        {
            if (player.CurrentPlayerModel == model)
            {
                return player.GetComponent<ItemControl>();
            }
        }
        return null;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, DISTANCE, layer) && Input.GetKeyDown(KeyCode.E))
        {
            itemController.ReceiveItem("Water Bottle");
            itemController.TakeAwayItem();
        }
    }
}
