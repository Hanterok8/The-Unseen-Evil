using Photon.Voice.Unity;
using System;
using UnityEngine;

public class VoiceChatActivator : MonoBehaviour
{
    [SerializeField] private Speaker speaker;
    public Action onMicrophoneStateChanged;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            speaker.enabled = !speaker.enabled;
            onMicrophoneStateChanged?.Invoke();
        }
    }
}
