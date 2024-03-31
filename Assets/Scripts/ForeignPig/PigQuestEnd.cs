using System;
using UnityEngine;

public class PigQuestEnd : MonoBehaviour
{
    private OnPigClicked pigClicked;
    private QuestSwitcher questSwitcher;
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
        Debug.Log("CHECK IS IN QUEST - " + questSwitcher.currentQuest.name);
        QuestData currentQuest = questSwitcher.currentQuest;
        if (currentQuest && currentQuest.name == "Foreign Pig")
        {
            ForcePigDisappear();
        }
    }

    private void ForcePigDisappear()
    {
        Debug.Log("Invoke");
        onQuestEnded?.Invoke();
        questSwitcher.AddQuestStep(1);
    }
}
