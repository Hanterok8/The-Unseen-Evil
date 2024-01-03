using Photon.Pun;

public class PlayerNickName : MonoBehaviourPunCallbacks
{
    private string _nickName;
    public string nickName => this._nickName;
    private CurrentPlayer _currentLivingPlayer;
    private PhotonView _photonView;
    private void Awake()
    {
        _currentLivingPlayer = FindObjectOfType<CurrentPlayer>();
        _photonView = _currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            _nickName = PhotonNetwork.NickName;
        }
    }
}
