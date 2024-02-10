using Photon.Pun;
using UnityEngine;

public class PlayerNickName : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _nickName;
    public string nickName => this._nickName;
    private PhotonView _photonView;
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            photonView.RPC(nameof(SetNickname), RpcTarget.All);
        }
    }
    [PunRPC]
    private void SetNickname()
    {
        _nickName = photonView.Owner.NickName;
    }
}
