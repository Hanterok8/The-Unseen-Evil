using UnityEngine;

public class OnAnomalyEliminated : MonoBehaviour
{
    private TouchingAnomaly touchingAnomaly;
    private QuestSwitcher questSwitcher;
    void Start()
    {
        GameObject currentPlayerModel = GetComponent<CurrentPlayer>().CurrentPlayerModel;
        touchingAnomaly = currentPlayerModel.GetComponent<TouchingAnomaly>();
        questSwitcher = GetComponent<QuestSwitcher>();
        touchingAnomaly.onAnomalyTouched += AddQuestProgress;
    }
    private void AddQuestProgress()
    {
        if (questSwitcher.currentQuest.name == "Anomaly Eliminator")
        {
            questSwitcher.AddQuestStep(1);
        }
    }
}
