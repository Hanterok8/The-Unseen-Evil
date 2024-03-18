using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TotalCompletedQuestsCounter : MonoBehaviour
{
    public int TotalQuestsDone; //{ get; private set; }

    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        TotalQuestsDone = 0;
    }
    public void UpdateTotalQuestsCount(int questsUpdated)
    {
        photonView.RPC(nameof(UpdateTotalQuestsCountRPC), RpcTarget.All, questsUpdated);
    }
    [PunRPC]
    private void UpdateTotalQuestsCountRPC(int questsUpdated)
    {
        TotalQuestsDone += questsUpdated;
    }
}
