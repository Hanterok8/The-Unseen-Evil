using Photon.Pun;
public class VoiceModeChanger : MonoBehaviourPunCallbacks
{
    public PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

}
