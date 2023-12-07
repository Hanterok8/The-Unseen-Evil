using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Runtime.CompilerServices;

public class StaminaSettings : NetworkBehaviour
{
    [SerializeField] private Image _staminaUI;
    private int _playerStamina = 100;
    void ChangeStaminaValue(int hpSubtract)
    {
        _playerStamina -= hpSubtract;
        float staminaUIwidth = _staminaUI.rectTransform.sizeDelta.x;
        _staminaUI.rectTransform.localPosition += -Vector3.right * staminaUIwidth/100.0f * hpSubtract;
        if (_playerStamina <= 0)
        {
            _playerStamina = 0;
        }
    }
    public void UpgradeStaminaMaxValue(int extraWidth)
    {
        Vector2 staminaUIsize = _staminaUI.rectTransform.sizeDelta;
        _staminaUI.rectTransform.sizeDelta = new Vector2(staminaUIsize.x+extraWidth, staminaUIsize.y);
    }
}
