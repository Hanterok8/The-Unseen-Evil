using Photon.Pun;
using UnityEngine;
public class PlayerOrDemon : MonoBehaviour
{
    [SerializeField] public bool isDemon;

    private PhotonView photonView;
    private string demonNickname;
    private bool isChecked = false;
    private void Start()
    {
        photonView = GetComponent<CurrentPlayer>().CurrentPlayerModel.GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (photonView.IsMine && !isChecked)
        {
            demonNickname = GetDemonNickname();
            if (PhotonNetwork.NickName == demonNickname)
            {
                isDemon = true;
            }
            isChecked = true;
        }
    }
    public string GetDemonNickname()
    {
        return PlayerPrefs.GetString("Demon");
    }

}
