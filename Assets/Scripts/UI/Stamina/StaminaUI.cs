using Photon.Pun;
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

    private void Start()
    {
        _playerEmptyObject = GameObject.FindGameObjectWithTag("PlayerInstance");
        _playerModel = GameObject.FindGameObjectWithTag("Player");
        _staminaTextUI = GameObject.FindGameObjectWithTag("StaminaText").GetComponent<TMP_Text>();
        _staminaUI = GetComponent<Image>();
    }
    private void Update()
    {
        if (_playerModel == null) _playerModel = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(_playerModel.GetComponent<PhotonView>().Owner.NickName);
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
