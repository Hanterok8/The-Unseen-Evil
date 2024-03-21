using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(gameObject);
        }
    }
}
