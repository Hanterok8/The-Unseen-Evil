using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControll : MonoBehaviour
{
    public string volume1Parametr = "MasterVolume";
    public string volume2Parametr = "MasterVolume";
    public string volume3Parametr = "MasterVolume";
    public string volume4Parametr = "MasterVolume";
    public string volume5Parametr = "MasterVolume";
    public string volume6Parametr = "MasterVolume";
    public string volume7Parametr = "MasterVolume";
    public string volume8Parametr = "MasterVolume";
    public string volume9Parametr = "MasterVolume";
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
        audioMixer.SetFloat(volume4Parametr, volumeValue);
        audioMixer.SetFloat(volume5Parametr, volumeValue);
        audioMixer.SetFloat(volume6Parametr, volumeValue);
        audioMixer.SetFloat(volume7Parametr, volumeValue);
        audioMixer.SetFloat(volume8Parametr, volumeValue);
        audioMixer.SetFloat(volume9Parametr, volumeValue);
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
