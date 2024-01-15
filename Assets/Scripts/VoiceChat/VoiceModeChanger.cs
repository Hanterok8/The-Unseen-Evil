using Photon.Voice.Unity;
using UnityEngine;
using Photon.Pun;
public class VoiceModeChanger : MonoBehaviourPunCallbacks
{
    private Speaker[] speakers;

    private void Start()
    {
        speakers = FindObjectsOfType<Speaker>();
    }
    public void TurnVoiceChatInto(PhotonView photonView, bool VoiceChatTurnInto)
    => photonView.RPC(nameof(TurnVoiceChatIntoRPC), RpcTarget.All, VoiceChatTurnInto);
    [PunRPC]
    private void TurnVoiceChatIntoRPC(bool VoiceChatTurnInto)
    {
        foreach (Speaker speaker in speakers)
        {
            speaker.enabled = VoiceChatTurnInto;
        }
    }
}
