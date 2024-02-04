using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControll : MonoBehaviour
{
    public string volume1Parametr = "MasterVolume";
    public string volume2Parametr = "MasterVolume";
    public string volume3Parametr = "MasterVolume";
    public AudioMixer audioMixer;
    public Slider slider;

    private const float _multiplier = 20f;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSlider);
    }
    private void HandleSlider(float value)
    {
        var volumeValue = CalculateVolume(value);
        audioMixer.SetFloat(volume1Parametr, volumeValue);
        audioMixer.SetFloat(volume2Parametr, volumeValue);
        audioMixer.SetFloat(volume3Parametr, volumeValue);
    }
    private float CalculateVolume(float value)
    {
        if(value == 0)
        {
            return -80;
        }
        return Mathf.Log10(value) * _multiplier;
    }
}
