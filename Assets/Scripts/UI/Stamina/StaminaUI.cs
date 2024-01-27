using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
//[RequireComponent(typeof(PhotonView))]
public class StaminaUI : MonoBehaviour
{
    private TMP_Text _staminaTextUI;
    private Image _staminaUI;
    private GameObject _playerModel;
    private GameObject _playerEmptyObject;

    private PlayerOrDemon playerOrDemon;
    private StaminaSettings staminaSettings;
    private void Start()
    {
        _playerEmptyObject = GameObject.FindGameObjectWithTag("PlayerInstance");
        _playerModel = GameObject.FindGameObjectWithTag("Player");
        _staminaTextUI = GameObject.FindGameObjectWithTag("StaminaText").GetComponent<TMP_Text>();
        _staminaUI = GetComponent<Image>();
        playerOrDemon = _playerEmptyObject.GetComponent<PlayerOrDemon>();
        staminaSettings = _playerModel.GetComponent<StaminaSettings>();
    }
    private void Update()
    {
        if (_playerModel == null)
        {
            _playerModel = GameObject.FindGameObjectWithTag("Player");
            staminaSettings = _playerModel.GetComponent<StaminaSettings>();
        }

        if (playerOrDemon.isDemon)
        {
            _staminaTextUI.text = "inf.";
            enabled = false;
            return;
        }
        _staminaUI.fillAmount = staminaSettings._playerStamina / 100.0f;
        _staminaTextUI.text = $"{staminaSettings._playerStamina}%";
    }
}
