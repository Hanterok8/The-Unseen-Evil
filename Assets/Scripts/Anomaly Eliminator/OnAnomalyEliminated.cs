using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnomalyEliminated : MonoBehaviour
{
    private TouchingAnomaly touchingAnomaly;
    private QuestSwitcher questSwitcher;
    private const int COINS_FOR_QUEST = 1;
    void Start()
    {
        GameObject currentPlayerModel = GetComponent<CurrentPlayer>().CurrentPlayerModel;
        touchingAnomaly = currentPlayerModel.GetComponent<TouchingAnomaly>();
        questSwitcher = GetComponent<QuestSwitcher>();
        touchingAnomaly.onAnomalyTouched += AddQuestProgress;
    }
    private void AddQuestProgress()
    {
        questSwitcher.AddQuestStep(1);
        if (questSwitcher.interactedTargets >= questSwitcher.currentQuest.requiredTargets)
        {
            questSwitcher.PassQuest(COINS_FOR_QUEST);
        }
    }
}
