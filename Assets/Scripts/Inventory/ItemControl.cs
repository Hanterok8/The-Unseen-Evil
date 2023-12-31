using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Image[] _inventorySprites;
    [SerializeField] private float _distance;
    private Transform[] _slots;
    private Transform[] _outlines;
    private PhotonView _photonView;
    private CurrentPlayer _currentLivingPlayer;
    private Camera _camera;
    private int lastSlot;
    private int selected;

    private bool moved = false;
    void Start()
    {
        _currentLivingPlayer = Object.FindObjectOfType<CurrentPlayer>();
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
    }
    void Update()
    {
        if (_photonView == null) _photonView = _currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        if (_camera == null) _camera = Camera.main;
        if (!_photonView.IsMine) return;
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, _distance, _layer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpInformation _itemInfo = hit.collider.gameObject.GetComponent<PickUpInformation>();
                ItemReceive(_itemInfo.name);
                Destroy(_itemInfo.gameObject);
            }
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        

        selected = -1;
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown($"{i + 1}"))
            {
                selected = i;
            }
        }

        if (selected != -1)
        {
            _outlines[lastSlot].gameObject.SetActive(false);
            _outlines[selected].gameObject.SetActive(true);
            lastSlot = selected;
        }


    }
    public void ItemReceive(string infoName)
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
        Image img = infoName switch
        {
            "AK-74" => _inventorySprites[0],
            "Empty bottle" => _inventorySprites[1],
            "cube" => _inventorySprites[2]
        }; ;
        img = Instantiate(img);
        img.transform.parent = _slots[freeSlotIndex];
        _slots[freeSlotIndex].GetComponent<SlotItemInformation>().name = infoName;
        img.transform.localPosition = new Vector3(0, 0, 0);
    }
}
