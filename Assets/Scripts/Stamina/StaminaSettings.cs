using System.Collections;
using UnityEngine;
[RequireComponent(typeof(ManController))]
public class StaminaSettings : MonoBehaviour
{
    [SerializeField] public int _playerStamina = 100;
    private bool _noDoubleCoroutines = false;
    private ManController moving;

    private void Start()
    {
        moving = GetComponent<ManController>();
        StartCoroutine(StaminaChanging());
    }
    private void Update()
    {
        //if (_playerStamina < 1) _playerStamina = 0;
        //if (_playerStamina > 99) _playerStamina = 100;
    }

    public void ChangeStaminaValue(int changedStamina) // якщо потрібно відняти ХП, то пишемо changedHP зі знаком "-".
    {
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
