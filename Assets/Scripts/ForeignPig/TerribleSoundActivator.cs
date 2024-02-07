using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerribleSoundActivator : MonoBehaviour
{
    private void OnEnable()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
