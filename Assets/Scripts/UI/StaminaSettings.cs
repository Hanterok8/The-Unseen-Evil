using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(ManController))]
public class StaminaSettings : MonoBehaviour
{
    [SerializeField] private Image _staminaUI;
    [SerializeField] private TMP_Text _staminaTextUI;
    [SerializeField] public int _playerStamina = 100;
    private bool _staminaSubtructing = false;
    private bool _noDoubleCoroutines = false;
    ManController moving;
    private void Start() => moving = GetComponent<ManController>();
    private void Update()
    {
        if (moving._isRunning && (moving.curSpeedX != 0 || moving.curSpeedY != 0) && !_noDoubleCoroutines && _playerStamina > 0) // && _staminaSubtructing
        {
            StartCoroutine(staminaWhileRunning());
        }
        else
        {
            if (!_noDoubleCoroutines) StartCoroutine(staminaWhileWalking());
        }

        if (_playerStamina < 1) _playerStamina = 0;
        if (_playerStamina > 99) _playerStamina = 100;
        _staminaTextUI.text = $"{_playerStamina}%";
    }

    public void ChangeStaminaValue(int changedHP) // якщо потрібно відняти ХП, то пишемо changedHP зі знаком "-".
    {
        _playerStamina += changedHP;
        _staminaUI.fillAmount = (float)_playerStamina / 100.0f;
        if (_playerStamina <= 0)
        {
            _playerStamina = 0;
            _staminaUI.fillAmount = 0;
        }
    }
    public IEnumerator staminaWhileRunning()
    {
        _noDoubleCoroutines = true;
        while (moving._isRunning && (moving.curSpeedX != 0 || moving.curSpeedY != 0))
        {
            _playerStamina -= 1;
            _staminaUI.fillAmount = (float)_playerStamina / 100.0f;
            yield return new WaitForSeconds(0.1f);

            //if (_playerStamina < 1) _playerStamina = 0; 
        }
        // _staminaSubtructing = false;
        _noDoubleCoroutines = false;
    }
    public IEnumerator staminaWhileWalking()
    {
        _noDoubleCoroutines = true;
        while ((!moving._isRunning && (moving.curSpeedX != 0 || moving.curSpeedY != 0)) || (moving.curSpeedX == 0 && moving.curSpeedY == 0))
        {
            _playerStamina += 1;
            _staminaUI.fillAmount = (float)_playerStamina / 100.0f;
            yield return new WaitForSeconds(0.15f);
            //if (_playerStamina > 99) _playerStamina = 100;
        }
        _noDoubleCoroutines = false;
        // _staminaSubtructing = true;
    }
}
