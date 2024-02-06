using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRecipeQuestActivatedChecker : MonoBehaviour
{
    private RecipeOpener recipeOpener;
    private QuestSwitcher questSwitcher;
    private const int COINS_FOR_QUEST = 1;

    void Start()
    {
        GameObject currentPlayerModel = GetComponent<CurrentPlayer>().CurrentPlayerModel;
        recipeOpener = currentPlayerModel.GetComponent<RecipeOpener>();
        questSwitcher = GetComponent<QuestSwitcher>();
        recipeOpener.onEnabledRecipeUI += CheckIsQuestActivated;
    }
    private void OnDisable()
    {
        recipeOpener.onEnabledRecipeUI -= CheckIsQuestActivated;
    }
    private void CheckIsQuestActivated()
    {
        if (questSwitcher.currentQuest && questSwitcher.currentQuest.name == "Recipe Of The Altar")
        {
            EndQuest();
        }
    }

    private void EndQuest()
    {
        questSwitcher.AddQuestStep(1);
        questSwitcher.PassQuest(COINS_FOR_QUEST);
    }
}
