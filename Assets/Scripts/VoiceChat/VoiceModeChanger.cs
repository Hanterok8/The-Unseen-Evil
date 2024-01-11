using Photon.Voice.Unity;
using UnityEngine;

public class VoiceModeChanger : MonoBehaviour
{
    //GameObject[] playersWithSpeaker;
    Speaker[] speakers;
    private void Start()
    {
        //Speaker[] speakers = FindObjectsOfType<Speaker>();
        speakers = FindObjectsOfType<Speaker>();
        //for (int i = 0; i < speakers.Length; i++)
        //    playersWithSpeaker[i] = speakers[i].gameObject;
    }
    public void TurnVoiceChatInto(bool VoiceChatTurnInto)
    {
        foreach (Speaker speaker in speakers)
        {
            speaker.enabled = VoiceChatTurnInto;
        }
    }
}
