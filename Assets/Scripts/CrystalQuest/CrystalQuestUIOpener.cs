using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalQuestUIOpener : OpenStore
{
    private CrystalElements crystalElements;
    private CharacterController characterController;
    private QuestSwitcher questSwitcher;
    private PhotonView photonView;
                                                                
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        questSwitcher = GetQuestSwitcher();
        characterController = GetComponent<CharacterController>();
        crystalElements = FindObjectOfType<CrystalElements>();
    }
    public override void SwapPlayerMovementState(bool enableTo)
    {
        base.SwapPlayerMovementState(enableTo);
        characterController.enabled = false;
        GameObject crystalCanvas = crystalElements.crystalCanvas;
        crystalCanvas.SetActive(true);
        characterController._playerAnimator.enabled = false;
        characterController.AnimatorStateChange(new Vector3(0, 0, 0));
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Crystal") && questSwitcher.currentQuest.name == "Lost Crystal" && photonView.IsMine)
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
