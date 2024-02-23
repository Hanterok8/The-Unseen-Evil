using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DomeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dome;
    private static int activatedLights = 0;

    private GameObject GetPlayerObject(GameObject model)
    {
        CurrentPlayer[] currentPlayers = FindObjectsOfType<CurrentPlayer>();
        foreach (CurrentPlayer currentPlayer in currentPlayers)
        {
            if (currentPlayer.CurrentPlayerModel == model)
            {
                return currentPlayer.gameObject;
            }
        }
        return null;
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
           GameObject playerEmpty = GetPlayerObject(collider.gameObject);
           QuestSwitcher questSwitcher = playerEmpty.GetComponent<QuestSwitcher>();
            if (questSwitcher.currentQuest.name == "Dome Light")
            {
                questSwitcher.AddQuestStep(1);
                transform.parent.GetComponent<Light>().enabled = true;
                activatedLights++;
                if (activatedLights == 4)
                {
                    dome.SetActive(true);
                }
            }
        }
    }
}
