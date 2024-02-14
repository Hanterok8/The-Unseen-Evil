using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class StaminaUI : MonoBehaviour
{
    private TMP_Text _staminaTextUI;
    private Image _staminaUI;
    private GameObject _playerModel;
    private CurrentPlayer currentPlayer;

    private StaminaSettings staminaSettings;
    private void OnEnable()
    {
        staminaSettings.onBecomeDemon += TurnOffStaminaUI;
        staminaSettings.onStaminaUpdated += UpdateStaminaUI;
    }
    private void OnDisable()
    {
        staminaSettings.onBecomeDemon -= TurnOffStaminaUI;
        staminaSettings.onStaminaUpdated -= UpdateStaminaUI;
    }
    private void Awake()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("PlayerInstance").GetComponent<CurrentPlayer>();
        _playerModel = GameObject.FindGameObjectWithTag("Player");
        _staminaTextUI = GameObject.FindGameObjectWithTag("StaminaText").GetComponent<TMP_Text>();
        _staminaUI = GetComponent<Image>();
        staminaSettings = _playerModel.GetComponent<StaminaSettings>();
    }
    private void Update()
    {
        if (_playerModel == null)
        {
            _playerModel = currentPlayer.CurrentPlayerModel;
            staminaSettings = _playerModel.GetComponent<StaminaSettings>();
        }
    }
    private void UpdateStaminaUI()
    {
        _staminaUI.fillAmount = (float)staminaSettings._playerStamina / (float)100.0f;
        _staminaTextUI.text = $"{staminaSettings._playerStamina}%";
    }
    private void TurnOffStaminaUI()
    {
        _staminaTextUI.text = "inf.";
        enabled = false;
    }
}
