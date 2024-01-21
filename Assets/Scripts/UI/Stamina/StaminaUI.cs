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
    //private PhotonView _photonView;
    //private bool arePlayersConnected;

    private void Start()
    {
        _playerEmptyObject = GameObject.FindGameObjectWithTag("PlayerInstance");
        _playerModel = GameObject.FindGameObjectWithTag("Player");
        _staminaTextUI = GameObject.FindGameObjectWithTag("StaminaText").GetComponent<TMP_Text>();
        _staminaUI = GetComponent<Image>();
        //InvokeRepeating(nameof(CheckArePlayersConnected), 0, 0.5f);
    }
    private void Update()
    {
        //if (arePlayersConnected)
        //{
        //    _playerEmptyObject = GetLocalPlayer();
        //}
        //else
        //{
        //    return;
        //}
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
    //private void CheckArePlayersConnected()
    //{
    //    GameObject[] playersInGame = GameObject.FindGameObjectsWithTag("PlayerInstance");
    //    if (playersInGame.Length == PlayerPrefs.GetInt("PlayerCount"))
    //    {
    //        arePlayersConnected = true;
    //        _photonView = GetComponent<PhotonView>();
    //        CancelInvoke();
    //    }
    //}
    //private GameObject GetLocalPlayer()
    //{
    //    GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("PlayerInstance");
    //    foreach (GameObject player in allPlayers)
    //    {
    //        if (player.GetComponent<PhotonView>().Owner.NickName == _photonView.Owner.NickName)
    //        {
    //            return player;
    //        }
    //    }
    //    return null;

    //}
    //private GameObject GetPlayerModel()
    //{
    //    return _playerEmptyObject.GetComponent<CurrentPlayer>().CurrentPlayerModel;
    //}
}
