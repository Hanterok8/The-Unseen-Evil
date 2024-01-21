using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
//[RequireComponent(typeof(PhotonView))]
public class BloodlustUI : MonoBehaviour
{
    [SerializeField] private Image _bloodlustUI;
    [SerializeField] private GameObject _player;
    private TMP_Text _bloodlustTextUI;
    private TMP_Text _bloodHintText;
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
    }
    private void Update()
    {

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
}
