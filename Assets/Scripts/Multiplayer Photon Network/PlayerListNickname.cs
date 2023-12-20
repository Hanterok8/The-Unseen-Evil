using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListNickname : MonoBehaviourPunCallbacks
{
    public void SetUpNickname(Player player)
    {
        GetComponent<TMP_Text>().text = player.NickName;
    }
}
