using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControll : MonoBehaviour
{
    public string volumeParametr = "MasterVolume";
    public AudioMixer audioMixer;
    public Slider slider;

    private const float _multiplier = 20f;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSlider);
    }
    private void HandleSlider(float value)
    {
        var volumeValue = Mathf.Log10(value) * _multiplier;
        audioMixer.SetFloat(volumeParametr, volumeValue);
    }
}
