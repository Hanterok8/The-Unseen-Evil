using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player"); 
        if (!_player.transform.parent.GetComponent<PlayerOrDemon>().isDemon)
        {
            GameObject.FindGameObjectWithTag("GameObjectBloodLust").SetActive(false);
            enabled = false;
            return;
        }
        _bloodlustTextUI = GameObject.FindGameObjectWithTag("BloodlustText").GetComponent<TMP_Text>();
        _bloodHintText = GameObject.FindGameObjectWithTag("BloodHint").GetComponent <TMP_Text>();
        _bloodlustUI = GetComponent<Image>();
    }
    private void Update()
    {
        BloodlustSettings bloodlustSettings = _player.GetComponent<BloodlustSettings>(); 
        _bloodlustUI.fillAmount = bloodlustSettings._demonBloodlust / 100.0f;
        _bloodlustTextUI.text = $"{bloodlustSettings._demonBloodlust}%";

        if (bloodlustSettings._demonBloodlust >= 60 && !bloodlustSettings.isAimodipsis)
        {
            _bloodHintText.text = "Press (F) to active Aimodipsis Mode.";
        }
        else
        {
            _bloodHintText.text = "";
        }
    }
}
