using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PersonController))]
public class StaminaSettings : MonoBehaviour
{
    [SerializeField] public int _playerStamina = 100;
    private bool _noDoubleCoroutines = false;
    private PersonController moving;

    private void Start() => moving = GetComponent<PersonController>();
    private void Update()
    {
        if (moving._isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0) && !_noDoubleCoroutines && _playerStamina > 0)
        {
            StartCoroutine(staminaWhileRunning());
        }
        else
        {
            if (!_noDoubleCoroutines) StartCoroutine(staminaWhileWalking());
        }

        if (_playerStamina < 1) _playerStamina = 0;
        if (_playerStamina > 99) _playerStamina = 100;
        
    }

    public void ChangeStaminaValue(int changedStamina) // якщо потрібно відняти ХП, то пишемо changedHP зі знаком "-".
    {
        _playerStamina += changedStamina;        
        if (_playerStamina <= 0)
        {
            _playerStamina = 0;
        }
    }
    public IEnumerator staminaWhileRunning()
    {
        _noDoubleCoroutines = true;
        while (moving._isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0))
        {
            _playerStamina -= 1;
            yield return new WaitForSeconds(0.1f);
        }
        _noDoubleCoroutines = false;
    }
    public IEnumerator staminaWhileWalking()
    {
        _noDoubleCoroutines = true;
        while ((!moving._isRunning && (moving.AxesSpeed.x != 0 || moving.AxesSpeed.y != 0)) || (moving.AxesSpeed.x == 0 && moving.AxesSpeed.y == 0))
        {
            _playerStamina += 1;
            yield return new WaitForSeconds(0.15f);
        }
        _noDoubleCoroutines = false;
    }
}
