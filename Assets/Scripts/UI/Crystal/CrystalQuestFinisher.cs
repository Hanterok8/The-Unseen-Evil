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
        CharacterController characterController = currentPlayerModel.GetComponent<CharacterController>();
        characterController._playerAnimator.enabled = true;
        characterController.enabled = true;
        TriggerController triggerController = currentPlayerModel.GetComponent<TriggerController>();
        triggerController.cameraController.enabled = true;
        triggerController.weapon.enabled = true;
        Destroy(gameObject);
    }
}
