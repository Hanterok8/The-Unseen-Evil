using Photon.Pun;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PersonController))]
public class StaminaSettings : MonoBehaviour
{
    [SerializeField] public int _playerStamina = 100;

    private PersonController moving;
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = transform.parent.GetComponent<PhotonView>();
        moving = GetComponent<PersonController>();
        StartCoroutine(StaminaChanging());  
    }

    public void ChangeStaminaValue(int changedStamina)
    {
        if (!_photonView.IsMine) return;
        _playerStamina += changedStamina;        
        if (_playerStamina <= 0)
        {
            _playerStamina = 0;
        }
    }
    public IEnumerator StaminaChanging()
    {
        while (true)
        {
            if (!_photonView.IsMine) break; 
            if (moving._isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0))
            {
                if (_playerStamina > 0) _playerStamina -= 1;
                yield return new WaitForSeconds(0.1f);
            }
            else if ((!moving._isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0)) || (moving.AxesSpeed.x == 0 && moving.AxesSpeed.y == 0))
            {
                if (_playerStamina < 100) _playerStamina += 1;
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
