using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
//[RequireComponent(typeof(PhotonView))]
public class BloodlustUI : MonoBehaviour
{
    [SerializeField] private Image bloodlustUI;
    [SerializeField] private GameObject player;
    [SerializeField] public GameObject hint;
    [SerializeField] public TMP_Text hintText;
    private TMP_Text _bloodlustTextUI;
    private PlayerOrDemon _playerOrDemon;
    private IsAimodipsis _aimodipsis;
    private GameObject _bloodlustScale;
    private BloodlustSettings bloodlustSettings;

    private void Awake()
    {
        _bloodlustScale = GameObject.FindGameObjectWithTag("GameObjectBloodLust");
        player = GameObject.FindGameObjectWithTag("PlayerInstance");
        _playerOrDemon = player.GetComponent<PlayerOrDemon>();
        _bloodlustTextUI = GameObject.FindGameObjectWithTag("BloodlustText").GetComponent<TMP_Text>();
        _aimodipsis = player.GetComponent<IsAimodipsis>();
        bloodlustSettings = player.GetComponent<BloodlustSettings>();
        _bloodlustScale.SetActive(false);
    }
    private void OnEnable()
    {
        bloodlustSettings.onBloodlustActivated += ActivateScale;
        bloodlustSettings.onChangedBloodlust += UpdateBloodlustUI;
    }
    private void OnDisable()
    {
        bloodlustSettings.onBloodlustActivated -= ActivateScale;
        bloodlustSettings.onChangedBloodlust -= UpdateBloodlustUI;
    }
    private void ActivateScale()
    {
        _bloodlustScale.SetActive(true);
    }
    private void UpdateBloodlustUI()
    {
        bloodlustUI.fillAmount = bloodlustSettings._demonBloodlust / 100.0f;
        _bloodlustTextUI.text = $"{bloodlustSettings._demonBloodlust}%";

        if (bloodlustSettings._demonBloodlust >= 60 && !_aimodipsis.isAimodipsis)
        {
            hint.SetActive(true);
            hintText.text = "Press (F) to activate Aimodipsis Mode";
        }
        else
        {
            hintText.text = "";
            hint.SetActive(false);
        }
    }
}
