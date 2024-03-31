using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PhotonView))]
public class StaminaSettings : MonoBehaviour
{
    [SerializeField] public int _playerStamina = 100;
    public Action onBecomeDemon;
    public Action onStaminaUpdated;
    public bool isDemon = false;
    private CharacterController moving;
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _photonView = GetComponent<PhotonView>();
        moving = GetComponent<CharacterController>();
        _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine) return;
        StartCoroutine(ChangeStaminaValue());
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerInstance");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().Owner.NickName == _photonView.Owner.NickName)
            {
                if (player.GetComponent<PlayerOrDemon>().isDemon)
                {
                    enabled = false;
                }

                break;
            }
        }
    }
    
    private IEnumerator ChangeStaminaValue()
    {
        while (true)
        {
            if (moving.isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0))
            {
                if (_playerStamina > 0) UpdateStamina(-1);
                yield return new WaitForSeconds(0.1f);
            }
            else if ((!moving.isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0)) || (moving.AxesSpeed.x == 0 && moving.AxesSpeed.y == 0))
            {
                if (_playerStamina < 100) UpdateStamina(1);
                yield return new WaitForSeconds(0.15f);
            }
            if (isDemon)
            {
                onBecomeDemon?.Invoke();
                StopAllCoroutines();
            }
        }

    }
    private void UpdateStamina(int staminaStep)
    {
        _playerStamina += staminaStep;
        onStaminaUpdated?.Invoke();
    }
}
