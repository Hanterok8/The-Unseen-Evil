using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
//[RequireComponent(typeof(PhotonView))]
public class BloodlustUI : MonoBehaviour
{
    [SerializeField] private Image _bloodlustUI;
    private TMP_Text _bloodlustTextUI;
    private TMP_Text _bloodHintText;
    private GameObject _player;
    private PlayerOrDemon _playerOrDemon;
    private IsAimodipsis _aimodipsis;
    private GameObject _bloodlustScale;
    //private PhotonView _photonView;
    //private bool arePlayersConnected;

    private void Start()
    {
        _bloodlustScale = GameObject.FindGameObjectWithTag("GameObjectBloodLust");
        _player = GameObject.FindGameObjectWithTag("PlayerInstance");
        _playerOrDemon = _player.GetComponent<PlayerOrDemon>();
        _bloodlustTextUI = GameObject.FindGameObjectWithTag("BloodlustText").GetComponent<TMP_Text>();
        _bloodHintText = GameObject.FindGameObjectWithTag("BloodHint").GetComponent<TMP_Text>();
        _aimodipsis = _player.GetComponent<IsAimodipsis>();
        //_photonView = GetComponent<PhotonView>();
        //InvokeRepeating(nameof(CheckArePlayersConnected), 0, 0.5f);
    }
    private void Update()
    {
        //if (arePlayersConnected)
        //{
        //    _player = GetLocalPlayer();
        //}
        //else
        //{
        //    return;
        //}
        //if (!_photonView.IsMine) return;
        //Debug.Log(_player.gameObject.GetComponent<PhotonView>().Owner.NickName);
        _bloodlustScale.SetActive(_playerOrDemon.isDemon);
        if (!_playerOrDemon.isDemon) return;
        BloodlustSettings bloodlustSettings = _player.GetComponent<BloodlustSettings>();
        _bloodlustUI.fillAmount = bloodlustSettings._demonBloodlust / 100.0f;
        _bloodlustTextUI.text = $"{bloodlustSettings._demonBloodlust}%";

        if (bloodlustSettings._demonBloodlust >= 60 && !_aimodipsis.isAimodipsis)
        {
            _bloodHintText.text = "Press (F) to active Aimodipsis Mode.";
        }
        else
        {
            _bloodHintText.text = "";
        }
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
    //    GameObject[] allPlayers= GameObject.FindGameObjectsWithTag("PlayerInstance");
    //    foreach (GameObject player in allPlayers)
    //    {
    //        if (player.GetComponent<PhotonView>().Owner.NickName == _photonView.Owner.NickName)
    //        {
    //            return player;
    //        }
    //    }
    //    return null;

    //}
}
