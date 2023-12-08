using UnityEngine;
using UnityEngine.UI;

public class StaminaSettings : MonoBehaviour
{
    [SerializeField] private Image _staminaUI;
    [SerializeField] private int _playerStamina = 100;
    public void ChangeStaminaValue(int changedHP) // якщо потрібно відняти ХП, то пишемо changedHP зі знаком "-".
    {
        _playerStamina += changedHP;
        _staminaUI.fillAmount = (float)_playerStamina/100.0f;
        if (_playerStamina <= 0)
        {
            _playerStamina = 0;
            _staminaUI.fillAmount = 0;
        }
    }
}
