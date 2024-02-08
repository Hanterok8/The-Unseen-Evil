using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestProgressBar : MonoBehaviour
{
    private QuestSwitcher questSwitcher;
    private Image bar;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerInstance");
        questSwitcher = player.GetComponent<QuestSwitcher>();
        bar = GetComponent<Image>();
        questSwitcher.onAddedQuestStep += UpdateProgressBar;
        questSwitcher.onGivenQuest += SetNewMaximum;
    }
    private void OnDisable()
    {
        questSwitcher.onAddedQuestStep -= UpdateProgressBar;
        questSwitcher.onGivenQuest -= SetNewMaximum;
    }
    private void SetNewMaximum()
    {
        bar.fillAmount = 0;
    }

    private void UpdateProgressBar()
    {
        QuestData currentQuest = questSwitcher.currentQuest;
        bar.fillAmount = (float)questSwitcher.interactedTargets / (float)currentQuest.requiredTargets;
    }

}
