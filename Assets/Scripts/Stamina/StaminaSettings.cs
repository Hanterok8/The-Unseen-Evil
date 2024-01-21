using Photon.Pun;
using UnityEngine;
[RequireComponent(typeof(PersonController))]
[RequireComponent(typeof(PhotonView))]
public class StaminaSettings : MonoBehaviour
{
    [SerializeField] public int _playerStamina = 100;
    private GameObject emptyPlayerObject;
    private PersonController moving;
    private PhotonView _photonView;
    private float _bootDelay;
    public bool isDemon = false;

    private void Start()
    {
        emptyPlayerObject = GameObject.FindGameObjectWithTag("PlayerInstance");
        _photonView = GetComponent<PhotonView>();
        moving = GetComponent<PersonController>();
        if (!_photonView.IsMine) return;
        InvokeRepeating(nameof(ChangeStaminaValue), 0, _bootDelay);
    }
    private void ChangeStaminaValue()
    {
        if (moving.isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0))
        {
            if (_playerStamina > 0) _playerStamina -= 1;
            _bootDelay = 0.1f;
        }
        else if ((!moving.isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0)) || (moving.AxesSpeed.x == 0 && moving.AxesSpeed.y == 0))
        {
            if (_playerStamina < 100) _playerStamina += 1;
            _bootDelay = 0.15f;
        }
        if (isDemon)
        {
            CancelInvoke();
        }
    }
}
