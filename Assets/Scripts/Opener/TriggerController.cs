using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private GameObject Player;
    private StoreOpener StoreIOpener;
    private CrystalQuestUIOpener CrystalIOpener;
    private bool isInShopCircleCollider = false;
    private IsAimodipsis aimodipsis;
    public bool isStoreOpened = false;
    private QuestSwitcher questSwitcher;
    private PhotonView photonView;
    [SerializeField] public Weapon weapon;
    [SerializeField] public CameraController cameraController;
    private void Start()
    {
        Player = GetPlayer();
        photonView = GetComponent<PhotonView>();
        StoreIOpener = GetComponent<StoreOpener>();
        CrystalIOpener = Player.GetComponent<CrystalQuestUIOpener>();
        aimodipsis = Player.GetComponent<IsAimodipsis>();
        questSwitcher = Player.GetComponent<QuestSwitcher>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInShopCircleCollider && !aimodipsis.isAimodipsis)
        {
            isStoreOpened = !isStoreOpened;
            OpenUI(isStoreOpened);
        }
    }
    private GameObject GetPlayer()
    {
        CurrentPlayer[] players = FindObjectsOfType<CurrentPlayer>();
        foreach (CurrentPlayer player in players)
        {
            if (player.CurrentPlayerModel == gameObject)
            {
                return player.gameObject;
            }
        }
        return null;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("shop_circle") && photonView.IsMine)
        {
            isInShopCircleCollider = true;
        }
        if (collider.gameObject.CompareTag("Crystal") && questSwitcher.currentQuest.name == "Lost Crystal" && photonView.IsMine)
        {
            CrystalIOpener.Open(true);
            Destroy(collider.transform.parent.gameObject);
            Destroy(collider.gameObject);
        }

    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("shop_circle") && photonView.IsMine)
        {
            isInShopCircleCollider = false;

            if (isStoreOpened)
            {
                OpenUI(false);
                cameraController.enabled = false;
                weapon.enabled = false;
            }

        }

    }
    private void OpenUI(bool isOpening)
    {
        StoreIOpener.Open(isOpening);
        cameraController.enabled = !isOpening;
        weapon.enabled = !isOpening;
    }
}
