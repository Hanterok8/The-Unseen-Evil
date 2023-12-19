using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform[] _slots;
    [SerializeField] private Transform[] _outlines;
    [SerializeField] private Image[] _inventorySprites;
    [SerializeField] private float _distance;
    private Camera _camera;
    private int lastSlot;
    private int selected;

    private bool moved = false;
    void Start()
    {
        _camera = Camera.main;
        selected = 0;
        lastSlot = 0;
        _outlines[selected].gameObject.SetActive(true);
    }
    void Update()
    {
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
        //if (Input.GetKeyDown(KeyCode.Alpha1)) selected = 0;
        //if (Input.GetKeyDown(KeyCode.Alpha2)) selected = 1;
        //if (Input.GetKeyDown(KeyCode.Alpha3)) selected = 2;
        //if (Input.GetKeyDown(KeyCode.Alpha4)) selected = 3;
        //if (Input.GetKeyDown(KeyCode.Alpha5)) selected = 4;
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
