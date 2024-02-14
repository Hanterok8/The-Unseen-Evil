using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalQuestFinisher : MonoBehaviour
{
    private GameObject player;
    public static int elementsPlaced;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerInstance");
        CrystalMover.onElementPlaced += CheckQuestProgress;
    }
    private void OnDisable()
    {
        CrystalMover.onElementPlaced -= CheckQuestProgress;
    }
    private void CheckQuestProgress()
    {
        elementsPlaced++;
        if (elementsPlaced == 5)
        {
            FinishQuest();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void FinishQuest()
    {
        QuestSwitcher questSwitcher = player.GetComponent<QuestSwitcher>();
        questSwitcher.AddQuestStep(1);
        GameObject currentPlayerModel = player.GetComponent<CurrentPlayer>().CurrentPlayerModel;
        currentPlayerModel.GetComponent<CharacterController>().enabled = true;
        OpenStore storeOpener = currentPlayerModel.GetComponent<OpenStore>();
        storeOpener.cameraController.enabled = true;
        storeOpener.weapon.enabled = true;
        Destroy(gameObject);
    }
}
