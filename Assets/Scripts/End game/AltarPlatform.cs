using Photon.Pun;
using UnityEngine;

public class AltarPlatform : MonoBehaviour
{
    public string requiredItem;
    private bool isPlayerEnteredPlatform = false;
    private GameObject platformItem;
    private ItemControl itemControl;
    private PhotonView photonView;
    private MeshRenderer meshRenderer;
    private GameObject player;
    private void Start()
    {
        CurrentPlayer currentPlayer = FindObjectOfType<CurrentPlayer>();
        photonView = currentPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        player = currentPlayer.gameObject;
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
        if (itemControl._slots[usingSlot].GetComponent<SlotItemInformation>().name != requiredItem && !platformItem.activeSelf)
            return;
        itemControl.TakeAwayItem();
        photonView.RPC(nameof(PlaceItemOntoPlatformRPC), RpcTarget.All);
    }

    [PunRPC]
    private void PlaceItemOntoPlatformRPC()
    {
        meshRenderer.material.color = Color.blue;
        platformItem.SetActive(true);
        Transform altarObject = transform.parent;
        altarObject.GetComponent<AltarConnectingItems>().AddActivatedPlatform(player);
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
