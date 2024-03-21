using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SoulsActivator : MonoBehaviour
{
    [SerializeField] private GameObject souls;
    private PhotonView _photonView;
    private QuestSwitcher playerQuestSwitcher;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine) return;
        QuestSwitcher[] questSwitchers = FindObjectsOfType<QuestSwitcher>();
        foreach (QuestSwitcher questSwitcher in questSwitchers)
        {
            if (questSwitcher.GetComponent<PhotonView>().Owner.NickName == _photonView.Owner.NickName)
            {
                playerQuestSwitcher = questSwitcher;
            }
        }

        playerQuestSwitcher.onGivenQuest += ActivateSouls;
    }

    private void OnDisable()
    {
        playerQuestSwitcher.onGivenQuest -= ActivateSouls;
    }

    private void ActivateSouls()
    {
        if (playerQuestSwitcher.currentQuest.name == "Maruntian Soul Catcher")
        {
            souls.SetActive(true);
        }
    }
}
