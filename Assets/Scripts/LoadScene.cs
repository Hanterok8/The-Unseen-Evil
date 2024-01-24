using Photon.Pun;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviourPunCallbacks
{
    public void Load(int level)
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(level);
    }
}
