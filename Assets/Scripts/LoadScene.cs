using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _Setting;
    public void Load(string level)
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(level);
    }
    public void OnSetting()
    {
        _Setting.SetActive(true);
    }
}
