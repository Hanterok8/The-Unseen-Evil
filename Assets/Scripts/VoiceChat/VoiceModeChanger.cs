using Photon.Pun;
using Photon.Voice.Unity;
public class VoiceModeChanger : MonoBehaviourPunCallbacks
{
    public PhotonView photonView;
    private Speaker[] speakers;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }



}
