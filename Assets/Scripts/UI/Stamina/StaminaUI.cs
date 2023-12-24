using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class StaminaUI : MonoBehaviour
{
    private TMP_Text _staminaTextUI;
    private Image _staminaUI;
    private GameObject _player;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _staminaTextUI = GameObject.FindGameObjectWithTag("StaminaText").GetComponent<TMP_Text>();
        _staminaUI = GetComponent<Image>();

    }
    private void Update()
    {
        
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
        enabled = !_player.transform.parent.GetComponent<PlayerOrDemon>().isDemon;
        StaminaSettings staminaSettings = _player.GetComponent<StaminaSettings>();
        _staminaUI.fillAmount = staminaSettings._playerStamina / 100.0f; 
        _staminaTextUI.text = $"{staminaSettings._playerStamina}%";
    }
}
