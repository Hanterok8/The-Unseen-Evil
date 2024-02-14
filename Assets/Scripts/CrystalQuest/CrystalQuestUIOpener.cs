using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalQuestUIOpener : OpenStore
{
    private CrystalElements crystalElements;
    private PersonController personController;
    private QuestSwitcher questSwitcher;

    private void Start()
    {
        questSwitcher = GetQuestSwitcher();
        personController = GetComponent<PersonController>();
        crystalElements = FindObjectOfType<CrystalElements>();
    }
    public override void SwapPlayerMovementState(bool enableTo)
    {
        base.SwapPlayerMovementState(enableTo);
        personController.enabled = false;
        GameObject crystalCanvas = crystalElements.crystalCanvas;
        crystalCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Crystal") && questSwitcher.currentQuest.name == "Lost Crystal")
        {
            SwapPlayerMovementState(false);
            Destroy(collider.transform.parent.gameObject);
            Destroy(collider.gameObject);
        }
    }
    private QuestSwitcher GetQuestSwitcher()
    {
        CurrentPlayer[] currentPlayers = FindObjectsOfType<CurrentPlayer>();
        foreach (CurrentPlayer currentPlayer in currentPlayers)
        {
            if (currentPlayer.CurrentPlayerModel == gameObject)
            {
                return currentPlayer.GetComponent<QuestSwitcher>();
            }
        }
        return null;
    }
}
