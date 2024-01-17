using UnityEngine;
using Photon.Pun;

public class IsAimodipsis : MonoBehaviour, IPunObservable 
{

    //public static bool isAimodipsis;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isAimodipsis);
        }
        else
        {
            isAimodipsis = (bool)stream.ReceiveNext();
        }
    }
    public bool isAimodipsis;
    private void Start()
    {
        isAimodipsis = false;
    }
}
