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

    private void OnEnable()
    {
        onBloodlustActivated?.Invoke();
        if (!_photonView.IsMine) return;
        StartCoroutine(ChangeBloodlustScale());
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
    private IEnumerator ChangeBloodlustScale()
    {
        while (true)
        {
            if (!_aimodipsis.isAimodipsis)
            {
                if (_demonBloodlust < 96) UpdateBloodlust(5);
                yield return new WaitForSeconds(8);
            }
            else
            {
                if (_demonBloodlust > 3) UpdateBloodlust(-4);
                yield return new WaitForSeconds(1);
            }
        }
    }
    private void UpdateBloodlust(int bloodlustStep)
    {
        _demonBloodlust += bloodlustStep;
        onChangedBloodlust?.Invoke();
    }
}
