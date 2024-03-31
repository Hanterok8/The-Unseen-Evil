using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ItemExchanger : MonoBehaviour
{
    private const int DISTANCE = 2;

    [SerializeField] private LayerMask layer;
    [SerializeField] private Camera _camera;
    private QuestSwitcher questSwitcher;
    private ItemControl itemController;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        itemController = GetPlayerItemController();
        questSwitcher = itemController.gameObject.GetComponent<QuestSwitcher>();
    }

    private ItemControl GetPlayerItemController()
    {
        CurrentPlayer[] currentPlayers = FindObjectsOfType<CurrentPlayer>();
        foreach (CurrentPlayer player in currentPlayers)
        {
            if (player.CurrentPlayerModel == gameObject)
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
                StartCoroutine(TakeAwayItemAndGetNewOne("Water Bottle"));
            }
            if (Physics.Raycast(ray, out hit, DISTANCE) && questSwitcher.currentQuest.name == "Secrets of the Roof")
            {
                GameObject frontItem = hit.collider.gameObject;
                if ((hit.collider.CompareTag("Chain") && IsCurrentItem("Wirecutters")) || (hit.collider.CompareTag("Plank") && IsCurrentItem("Axe")))
                {
                    frontItem.GetComponent<Rigidbody>().isKinematic = false;
                    itemController.TakeAwayItem();
                    questSwitcher.AddQuestStep(1);
                }
                else if (hit.collider.CompareTag("Roof door") && IsCurrentItem("Roof key"))
                {
                    frontItem.GetComponent<Animator>().SetTrigger("OpenDoor");
                    itemController.TakeAwayItem();
                    questSwitcher.AddQuestStep(1);
                }
                else if (hit.collider.CompareTag("Prayer2"))
                {
                    Destroy(frontItem);
                    questSwitcher.AddQuestStep(1);
                }
                
            }
            if (Physics.Raycast(ray, out hit, DISTANCE) && questSwitcher.currentQuest.name == "Missing pieces")
            {
                Debug.Log($"{hit.collider.CompareTag("Prayer1") || hit.collider.CompareTag("Prayer3")} \n Prayer1 - {hit.collider.CompareTag("Prayer1")} \n Prayer3 - {hit.collider.CompareTag("Prayer3")}");
                if (hit.collider.CompareTag("Prayer1") || hit.collider.CompareTag("Prayer3"))
                {
                    Destroy(hit.collider.gameObject);
                    questSwitcher.AddQuestStep(1);
                }
            }
            if (Physics.Raycast(ray, out hit, DISTANCE))
            {
                if (hit.collider.CompareTag("Sheep") && IsCurrentItem("Knife"))
                {
                    StartCoroutine(TakeAwayItemAndGetNewOne("Lamb's blood"));
                }
            }
            
        }
        
        
    }
    private bool IsCurrentItem(string tag)
    {
        return itemController._slots[itemController.selected].GetComponent<SlotItemInformation>().name == tag;
    }
    private IEnumerator TakeAwayItemAndGetNewOne(string itemName)
    {
        itemController.TakeAwayItem();
        yield return new WaitForEndOfFrame();
        itemController.ReceiveItem(itemName);
    }
}
