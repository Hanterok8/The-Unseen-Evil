using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class WaterReceiver : MonoBehaviour
{
    private const int DISTANCE = 2;

    [SerializeField] private GameObject model;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Camera _camera;
    private QuestSwitcher questSwitcher;
    private ItemControl itemController;
    private PhotonView photonView;

    private void Start()
    {
        photonView = model.GetComponent<PhotonView>();
        questSwitcher = model.GetComponent<QuestSwitcher>();
        itemController = GetPlayerItemController();
    }

    private ItemControl GetPlayerItemController()
    {
        CurrentPlayer[] currentPlayers = FindObjectsOfType<CurrentPlayer>();
        foreach (CurrentPlayer player in currentPlayers)
        {
            if (player.CurrentPlayerModel == model)
            {
                return player.GetComponent<ItemControl>();
            }
        }
        return null;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, DISTANCE, layer))
            {
                StartCoroutine(GetHolyWater());
            }
            else if (Physics.Raycast(ray, out hit, DISTANCE) && questSwitcher.currentQuest.name == "Secrets of the Roof")
            {
                GameObject frontItem = hit.collider.gameObject;
                if ((hit.collider.GetComponent<PickUpInformation>().name == "Chain" && IsCurrentItem("Wirecutters")) || (hit.collider.GetComponent<PickUpInformation>().name == "Plank" && IsCurrentItem("Axe")))
                {
                    frontItem.GetComponent<Rigidbody>().isKinematic = false;
                    questSwitcher.AddQuestStep(1);
                }
                else if (hit.collider.GetComponent<PickUpInformation>().name == "Roof door" && IsCurrentItem("Roof key"))
                {
                    frontItem.GetComponent<Animator>().SetTrigger("OpenDoor");
                    questSwitcher.AddQuestStep(1);
                }
                else if (hit.collider.GetComponent<PickUpInformation>().name == "Prayer2")
                {
                    Destroy(frontItem);
                    questSwitcher.AddQuestStep(1);
                }
            }
        }
        
        
    }
    private bool IsCurrentItem(string tag)
    {
        return itemController._slots[itemController.selected].GetComponent<SlotItemInformation>().name == tag;
    }
    private IEnumerator GetHolyWater()
    {
        itemController.ReceiveItem("Water Bottle");
        yield return null;
        itemController.TakeAwayItem();
    }
}
