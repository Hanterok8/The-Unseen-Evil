using Photon.Voice.Unity;
using System;
using UnityEngine;
using Photon.Voice;

public class VoiceChatActivator : MonoBehaviour
{
    [SerializeField] private GameObject speaker;
    public Action onMicrophoneStateChanged;
    private Recorder voiceRecorder;

    private void Start()
    {
        voiceRecorder = GameObject.FindGameObjectWithTag("VoiceManager").GetComponent<Recorder>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // speaker.SetActive(!speaker.activeSelf);
            // onMicrophoneStateChanged?.Invoke();
            voiceRecorder.TransmitEnabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            voiceRecorder.TransmitEnabled = false;
        }
    }
}
