using Photon.Pun;
using System.Collections;
using UnityEngine;
using System;

public class BloodlustSettings : MonoBehaviourPunCallbacks
{
    public int _demonBloodlust;
    public Action onChangedBloodlust;
    public Action onBloodlustActivated;
    private PhotonView _photonView;
    private CurrentPlayer _currentLivingPlayer;
    private IsAimodipsis _aimodipsis;
    private float _bootDelay = 3;

    private void OnEnable()
    {
        onBloodlustActivated?.Invoke();
        if (!_photonView.IsMine) return;
        InvokeRepeating(nameof(ChangeBloodlustScale), 0, _bootDelay);
    }
    private void Awake()
    {
        _currentLivingPlayer = FindObjectOfType<CurrentPlayer>();
        _aimodipsis = FindObjectOfType<IsAimodipsis>();
        _demonBloodlust = 0;
        _photonView = _currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
    }
    void Update()
    {
        if (_photonView == null) _photonView = _currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
    }
    private void ChangeBloodlustScale()
    {
        if (!_aimodipsis.isAimodipsis)
        {
            if (_demonBloodlust < 96) UpdateBloodlust(5);
            _bootDelay = 8;
        }
        else
        {
            if (_demonBloodlust > 3) UpdateBloodlust(-4);
            _bootDelay = 1;
        }
    }
    private void UpdateBloodlust(int bloodlustStep)
    {
        _demonBloodlust += bloodlustStep;
        onChangedBloodlust?.Invoke();
    }
}
