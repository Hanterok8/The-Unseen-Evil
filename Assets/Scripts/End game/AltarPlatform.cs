using Photon.Pun;
using UnityEngine;

public class AltarPlatform : MonoBehaviour
{
    public string requiredItem;
    private bool isPlayerEnteredPlatform = false;
    private GameObject platformItem;
    private ItemControl itemControl;
    private PhotonView photonView;
    MeshRenderer meshRenderer;
    private void Start()
    {
        CurrentPlayer currentPlayer = FindObjectOfType<CurrentPlayer>();
        photonView = currentPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        GameObject player = currentPlayer.gameObject;
        itemControl = player.GetComponent<ItemControl>();
        platformItem = transform.GetChild(0).gameObject;
        platformItem.SetActive(false);
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.E) && isPlayerEnteredPlatform)
        {
            PlaceItemOntoPlatform();
        }
    }
    private void PlaceItemOntoPlatform()
    {
        int usingSlot = itemControl.selected;
        if (itemControl._slots[usingSlot].GetComponent<SlotItemInformation>().name != requiredItem)
            return;
        platformItem.SetActive(true);
        meshRenderer.material.color = Color.blue;
        itemControl.TakeAwayItem();
        Transform altarObject = transform.parent;
        altarObject.GetComponent<AltarConnectingItems>().AddActivatedPlatform();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isPlayerEnteredPlatform = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isPlayerEnteredPlatform = false;
    }
}
