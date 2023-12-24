using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNickName : MonoBehaviourPunCallbacks
{
    private string _nickName;
    public string nickName => this._nickName;
    private PhotonView _photonView;
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            _nickName = PhotonNetwork.NickName;
        }
    }
}
