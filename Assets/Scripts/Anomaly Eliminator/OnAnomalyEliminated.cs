using Photon.Pun;
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
    private void OnDisable()
    {
        touchingAnomaly.onAnomalyTouched -= AddQuestProgress;
    }
    private void AddQuestProgress(Collider collider)
    {
        if (questSwitcher.currentQuest.name == "Anomaly Eliminator")
        {
            collider.GetComponent<ParticleSystem>().loop = false;
            Destroy(collider.GetComponent<Light>());
            questSwitcher.AddQuestStep(1);
        }
    }
}
