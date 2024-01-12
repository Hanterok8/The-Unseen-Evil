using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
public class PlayerOrDemon : MonoBehaviour
{
    [SerializeField] public bool isDemon;

    private PhotonView photonView;
    private List<string> demonNicknames = new List<string>();
    private void Start()
    {
        photonView = GetComponent<CurrentPlayer>().CurrentPlayerModel.GetComponent<PhotonView>();
        if (!photonView.IsMine) return;
        GetDemonNicknames();
        for (int i = 0; i < demonNicknames.Count; i++)
        {
            if (PhotonNetwork.NickName == demonNicknames[i])
            {
                isDemon = true;
                break;
            }
        }
    }
    public void GetDemonNicknames()
    {
        int numberOfDemonPlayers = PlayerPrefs.GetInt("PlayerCount") / 3;
        if (numberOfDemonPlayers == 0) numberOfDemonPlayers++;
        for (int i = 0; i < numberOfDemonPlayers; i++)
        {
            Debug.Log(PlayerPrefs.GetString($"Demon{i}"));
            demonNicknames.Add(PlayerPrefs.GetString($"Demon{i}"));
        }
    }

}
