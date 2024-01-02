using Photon.Pun;
using UnityEngine;

public class AltarPlatform : MonoBehaviour
{
    [SerializeField] private int indexOfRequiredItem;
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
        if (itemControl._slots[itemControl.selected].GetComponent<SlotItemInformation>().name != requiredItem)
            return;
        platformItem.SetActive(true);
        meshRenderer.material.color = Color.blue;
        itemControl.TakeAwayItem(usingSlot);
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
