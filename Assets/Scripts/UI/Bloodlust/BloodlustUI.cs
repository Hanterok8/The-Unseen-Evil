using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BloodlustUI : MonoBehaviour
{
    private TMP_Text _bloodlustTextUI;
    private TMP_Text _bloodHintText;
    private Image _bloodlustUI;
    private GameObject _player;

    private IsAimodipsis _aimodipsis;

    private void Start()
    {

        _player = GameObject.FindGameObjectWithTag("PlayerInstance");
        Invoke(nameof(CheckIsDemon), 2f);
        _bloodlustTextUI = GameObject.FindGameObjectWithTag("BloodlustText").GetComponent<TMP_Text>();
        _bloodHintText = GameObject.FindGameObjectWithTag("BloodHint").GetComponent<TMP_Text>();
        _bloodlustUI = GetComponent<Image>();
        _aimodipsis = _player.GetComponent<IsAimodipsis>();
    }
    private void Update()
    {
        if (_player == null) _player = GameObject.FindGameObjectWithTag("PlayerInstance");
        BloodlustSettings bloodlustSettings = _player.GetComponent<BloodlustSettings>();
        _bloodlustUI.fillAmount = bloodlustSettings._demonBloodlust / 100.0f;
        _bloodlustTextUI.text = $"{bloodlustSettings._demonBloodlust}%";

        if (bloodlustSettings._demonBloodlust >= 60 && _aimodipsis.isAimodipsis)
        {
            _bloodHintText.text = "Press (F) to active Aimodipsis Mode.";
        }
        else
        {
            _bloodHintText.text = "";
        }
    }
    private void CheckIsDemon()
    {
        if (!_player.GetComponent<PlayerOrDemon>().isDemon)
        {
            GameObject.FindGameObjectWithTag("GameObjectBloodLust").SetActive(false);
            enabled = false;
        }
    }
}
