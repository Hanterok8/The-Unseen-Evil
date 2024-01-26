using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviourPunCallbacks
{

    public void Load(string level)
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(level);
    }
}
