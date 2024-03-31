using System.Collections;
using Photon.Pun;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] public Weapon weapon;
    [SerializeField] public CameraController cameraController;
    public bool isStoreOpened = false;
    
    private GameObject Player;
    private StoreOpener StoreUIOpener;
    private CrystalQuestUIOpener CrystalIOpener;
    private bool isInShopCircleCollider = false;
    private IsAimodipsis aimodipsis;
    [SerializeField] private QuestSwitcher questSwitcher;
    [SerializeField] private PhotonView photonView;
    private int cooldownTime = 0;
    
    private void Start()
    {
        Player = GetPlayer();
        photonView = GetComponent<PhotonView>();
        StoreUIOpener = GetComponent<StoreOpener>();
        CrystalIOpener = Player.GetComponent<CrystalQuestUIOpener>();
        aimodipsis = Player.GetComponent<IsAimodipsis>();
        questSwitcher = Player.GetComponent<QuestSwitcher>();
    }
    private void Update()
    {
        if (!photonView.IsMine) return;
        if (Input.GetKeyDown(KeyCode.E) && isInShopCircleCollider && !aimodipsis.isAimodipsis && cooldownTime <= 0)
        {
            isStoreOpened = !isStoreOpened;
            StoreUIOpener.Open();
            StopAllCoroutines();
            StartCoroutine(ResetCooldownTime());
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
        if (collider.gameObject.CompareTag("Crystal") && collider.GetComponent<PhotonView>().Owner.NickName == photonView.Owner.NickName && questSwitcher.currentQuest.name == "Lost Crystal" && photonView.IsMine)
        {
            CrystalIOpener.Open();
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
                StoreUIOpener.Open();
            }

        }

    }

    private IEnumerator ResetCooldownTime()
    {
        cooldownTime = 3;
        while (cooldownTime > 0)
        {
            yield return new WaitForSeconds(1);
            cooldownTime--;
        }
    }
}
