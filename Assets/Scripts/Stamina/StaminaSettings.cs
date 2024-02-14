using Photon.Pun;
using System;
using UnityEngine;
[RequireComponent(typeof(PhotonView))]
public class StaminaSettings : MonoBehaviour
{
    [SerializeField] public int _playerStamina = 100;
    public Action onBecomeDemon;
    public Action onStaminaUpdated;
    public bool isDemon = false;
    private GameObject emptyPlayerObject;
    private CharacterController moving;
    private PhotonView _photonView;
    private float _bootDelay = 0.1f;

    private void Start()
    {
        emptyPlayerObject = GameObject.FindGameObjectWithTag("PlayerInstance");
        _photonView = GetComponent<PhotonView>();
        moving = GetComponent<CharacterController>();
        _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine) return;
        InvokeRepeating(nameof(ChangeStaminaValue), 0, _bootDelay);
    }
    private void ChangeStaminaValue()
    {
        if (moving.isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0))
        {
            if (_playerStamina > 0) UpdateStamina(-1);
            _bootDelay = 0.1f;
        }
        else if ((!moving.isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0)) || (moving.AxesSpeed.x == 0 && moving.AxesSpeed.y == 0))
        {
            if (_playerStamina < 100) UpdateStamina(1);
            _bootDelay = 0.15f;
        }
        if (isDemon)
        {
            onBecomeDemon?.Invoke();
            CancelInvoke();
        }
    }
    private void UpdateStamina(int staminaStep)
    {
        onStaminaUpdated?.Invoke();
        _playerStamina += staminaStep;
    }
}
