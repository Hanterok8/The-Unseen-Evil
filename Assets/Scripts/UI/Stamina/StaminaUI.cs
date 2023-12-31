using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class StaminaUI : MonoBehaviour
{
    private TMP_Text _staminaTextUI;
    private Image _staminaUI;
    private GameObject _playerModel;
    private GameObject _playerEmptyObject;


    private void Start()
    {
        _playerModel = GameObject.FindGameObjectWithTag("Player");
        _playerEmptyObject = GameObject.FindGameObjectWithTag("PlayerInstance");
        _staminaTextUI = GameObject.FindGameObjectWithTag("StaminaText").GetComponent<TMP_Text>();
        _staminaUI = GetComponent<Image>();

    }
    private void Update()
    {
        if (_playerModel == null)  _playerModel = GameObject.FindGameObjectWithTag("Player"); 
        if (_playerEmptyObject.GetComponent<PlayerOrDemon>().isDemon)
        {
            _staminaTextUI.text = "inf.";
            return;
        }
        StaminaSettings staminaSettings = _playerModel.GetComponent<StaminaSettings>();
        _staminaUI.fillAmount = staminaSettings._playerStamina / 100.0f; 
        _staminaTextUI.text = $"{staminaSettings._playerStamina}%";
    }
}
