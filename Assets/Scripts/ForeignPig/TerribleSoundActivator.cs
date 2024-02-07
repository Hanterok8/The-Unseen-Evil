using UnityEngine;

public class TerribleSoundActivator : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private void OnEnable()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClip);
    }
}
