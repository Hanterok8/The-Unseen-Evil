using UnityEngine;

public class MicrophoneActivator : MonoBehaviour
{
    private VoiceChatActivator voiceChat;
    private GameObject microphone;

    private void Start()
    {
        voiceChat = FindObjectOfType<VoiceChatActivator>();
        microphone = transform.GetChild(0).gameObject;
        voiceChat.onMicrophoneStateChanged += ChangeMicrophoneState;
    }

    private void OnDisable()
    {
        voiceChat.onMicrophoneStateChanged -= ChangeMicrophoneState;
    }

    private void ChangeMicrophoneState()
    {
        microphone.SetActive(!microphone.activeSelf);
    }
}
