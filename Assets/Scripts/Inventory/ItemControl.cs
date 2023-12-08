using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
            //Тут буде відображатись, як гравець навівся на предмет
            Debug.Log(1);
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpInformation _itemInfo = hit.collider.gameObject.GetComponent<PickUpInformation>();
                ItemPickUp(_itemInfo);
            }

        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        

        selected = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1)) selected = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selected = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selected = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) selected = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) selected = 4;
        if (selected != -1)
        {
            _outlines[lastSlot].gameObject.SetActive(false);
            _outlines[selected].gameObject.SetActive(true);
            lastSlot = selected;
        }


    }
    void ItemPickUp(PickUpInformation info)
    {
        int freeSlotIndex = -1;
        for (int i = 0; i < _slots.Length; i++) // Знаходження вільного слоту.
        {
            if (_slots[i].childCount < 2)
            {
                freeSlotIndex = i;
                break;
            }
        }
        if (freeSlotIndex == -1) return;
        Image img = info.name switch
        {
            "apple" => _inventorySprites[0] // тестова назва
        };
        img = Instantiate(img);
        img.transform.parent = _slots[freeSlotIndex];
        _slots[freeSlotIndex].GetComponent<SlotItemInformation>().name = info.name;
        img.transform.localPosition = new Vector3(0, 0, 0);
        Destroy(info.gameObject);
    }
}
