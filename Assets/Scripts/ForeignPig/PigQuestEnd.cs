using System;
using UnityEngine;

public class PigQuestEnd : MonoBehaviour
{
    private OnPigClicked pigClicked;
    private QuestSwitcher questSwitcher;
    private const int COINS_FOR_QUEST = 2;
    public Action onQuestEnded;

    private void Start()
    {
        GameObject currentPlayerModel = GetComponent<CurrentPlayer>().CurrentPlayerModel;
        pigClicked = currentPlayerModel.GetComponent<OnPigClicked>();
        questSwitcher = GetComponent<QuestSwitcher>();
        pigClicked.onAPigClicked += CheckIsInQuest;
    }
    private void OnDisable()
    {
        pigClicked.onAPigClicked -= CheckIsInQuest;
    }
    private void CheckIsInQuest()
    {
        QuestData currentQuest = questSwitcher.currentQuest;
        if (currentQuest && currentQuest.name == "Foreign Pig")
        {
            ForcePigDisappear();
        }
    }

    private void ForcePigDisappear()
    {
        onQuestEnded?.Invoke();
        questSwitcher.AddQuestStep(1);
        questSwitcher.PassQuest(COINS_FOR_QUEST);
    }
}
