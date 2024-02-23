using Photon.Pun;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Image[] _inventorySprites;
    [SerializeField] private float _distance;
    [SerializeField] private GameObject[] _inventoryGameObjects;
    [SerializeField] private GameObject demonPrefab;
    public int selected;
    public Transform[] _slots;
    private Transform[] _outlines;
    private PhotonView _photonView;
    private CurrentPlayer _currentLivingPlayer;
    private Camera _camera;
    private int lastSlot;
    private int itemIndexInInventory;
    QuestSwitcher _questSwitcher;

    void Start()
    {
        _questSwitcher = GetComponent<QuestSwitcher>();
        _currentLivingPlayer = GetComponent<CurrentPlayer>();
        _photonView = _currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        _camera = Camera.main;
        selected = 0;
        lastSlot = 0;
        Transform slotsChildren = GameObject.FindGameObjectWithTag("Slots").transform;
        Transform outlineChildren = GameObject.FindGameObjectWithTag("Outlines").transform;
        _slots = new Transform[slotsChildren.childCount];
        _outlines = new Transform[outlineChildren.childCount];
        for (int i = 0; i < slotsChildren.childCount; i++)
        {
            _slots[i] = slotsChildren.GetChild(i);
            _outlines[i] = outlineChildren.GetChild(i);
        }
        _outlines[selected].gameObject.SetActive(true);
        _inventoryGameObjects = _currentLivingPlayer.CurrentPlayerModel.GetComponent<Items>().ItemGameObjects;
    }

    void Update()
    {

        if (_photonView == null) _photonView = _currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        if (_camera == null) _camera = Camera.main;
        if (!_photonView.IsMine) return;

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, _distance, _layer) && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem(hit);
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);


        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown($"{i + 1}"))
            {
                SelectAnotherSlot(i);
                break;
            }
        }




    }
    public virtual void PickUpItem(RaycastHit hit)
    {
        PickUpInformation _itemInfo = hit.collider.gameObject.GetComponent<PickUpInformation>();
        if (_itemInfo.isQuestedItem)
        {
            if (!_questSwitcher.currentQuest) return;
            foreach (string item in _questSwitcher.currentQuest.usingItems)
            {
                if (item == _itemInfo.name)
                {
                    ReceiveItem(_itemInfo.name);
                    Destroy(_itemInfo.gameObject);
                }
            }
            return;
        }

        ReceiveItem(_itemInfo.name);
        if(!_itemInfo.gameObject.isStatic)
            Destroy(_itemInfo.gameObject);
    }
    public void ReceiveItem(string infoName)
    {
        int freeSlotIndex = -1;
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].childCount < 1)
            {
                freeSlotIndex = i;
                break;
            }
        }
        if (freeSlotIndex == -1) return;
        if (_inventoryGameObjects[0] == null && _currentLivingPlayer.CurrentPlayerModel != demonPrefab)
        {
            _inventoryGameObjects = _currentLivingPlayer.CurrentPlayerModel.GetComponent<Items>().ItemGameObjects;
        }
        _inventoryGameObjects[itemIndexInInventory].SetActive(false);
        itemIndexInInventory = infoName switch
        {
            "AK-74" => 0,
            "Empty bottle" => 1,
            "Lamb's blood" => 2,
            "Gift of foresight" => 3,
            "Maze key" => 4,
            "Axe" => 5,
            "Roof key" => 6,
            "Wirecutters" => 7,
            "Water Bottle" => 8
        };
        Image img = _inventorySprites[itemIndexInInventory];
        _inventoryGameObjects[itemIndexInInventory].SetActive(true);
        img = Instantiate(img);
        img.transform.parent = _slots[freeSlotIndex];
        SlotItemInformation slotInformation = _slots[freeSlotIndex].GetComponent<SlotItemInformation>();
        slotInformation.name = infoName;
        slotInformation.itemGameObjectIndex = itemIndexInInventory;
        img.transform.localPosition = new Vector3(0, 0, 0);
        SelectAnotherSlot(freeSlotIndex);
    }
    public void TakeAwayItem()
    {
        int slotIndexOfItem = selected;
        int currentGameObjectIndex = _slots[slotIndexOfItem].GetComponent<SlotItemInformation>().itemGameObjectIndex;
        _inventoryGameObjects[currentGameObjectIndex].SetActive(false);
        SlotItemInformation slotInformation = _slots[slotIndexOfItem].GetComponent<SlotItemInformation>();
        slotInformation.name = "";
        slotInformation.itemGameObjectIndex = -1;
        Transform itemImage = _slots[slotIndexOfItem].transform.GetChild(0);
        Destroy(itemImage.gameObject);
    }
    private void SelectAnotherSlot(int newSlotIndex)
    {
        if (_inventoryGameObjects.Length == 0 || _inventoryGameObjects == null)
        {
            _inventoryGameObjects = _currentLivingPlayer.CurrentPlayerModel.GetComponent<Items>().ItemGameObjects;
        }
        selected = newSlotIndex;
        _outlines[lastSlot].gameObject.SetActive(false);
        _outlines[selected].gameObject.SetActive(true);
        int indexOfPickedGameObject = _slots[selected].GetComponent<SlotItemInformation>().itemGameObjectIndex;
        int indexOfLastGameObject = _slots[lastSlot].GetComponent<SlotItemInformation>().itemGameObjectIndex;
        if (indexOfLastGameObject != -1) _inventoryGameObjects[indexOfLastGameObject].SetActive(false);

        if (indexOfPickedGameObject != -1) _inventoryGameObjects[indexOfPickedGameObject].SetActive(true);
        lastSlot = selected;
    }
}
