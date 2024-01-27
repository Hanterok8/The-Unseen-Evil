using Photon.Pun;
using System.Collections;
using UnityEngine;

public class BloodlustSettings : MonoBehaviourPunCallbacks
{
    public int _demonBloodlust;
    private PhotonView _photonView;
    private CurrentPlayer _currentLivingPlayer;
    private IsAimodipsis _aimodipsis;
    private float _bootDelay = 3;
    private void OnEnable()
    {
        _currentLivingPlayer = FindObjectOfType<CurrentPlayer>();
        _aimodipsis = FindObjectOfType<IsAimodipsis>();
        Invoke(nameof(OffForInhabitant), 2);
        _demonBloodlust = 0;
        _photonView = _currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        if (!_photonView.IsMine) return;
        InvokeRepeating(nameof(ChangeBloodlustScale), 0, _bootDelay);
    }
    void Update()
    {
        if (_photonView == null) _photonView = _currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
    }
    private void OffForInhabitant()
    {
        if (!GetComponent<PlayerOrDemon>().isDemon)
        {
            enabled = false;
        }
    }
    private void ChangeBloodlustScale()
    {
        if (!_aimodipsis.isAimodipsis)
        {
            if (_demonBloodlust < 100) _demonBloodlust += 50;
            _bootDelay = 3;
        }
        else
        {
            if (_demonBloodlust > 0) _demonBloodlust -= 2;
            _bootDelay = 1;
        }
    }
}
