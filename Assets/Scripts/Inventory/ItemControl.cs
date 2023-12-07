using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemControl : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform[] _slots;
    [SerializeField] private int[] _amount;
    [SerializeField] private Image[] _inventorySprites;
    [SerializeField] private float _distance;
    private Camera _camera;
    private int lastSlot;
    private int selected;

    private bool moved = false;
    void Start()
    {
        _camera = Camera.main;
        _amount = new int[_slots.Length];
        selected = 0;
        lastSlot = 0;
        _slots[selected].GetChild(1).gameObject.SetActive(true);
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
        if (selected != -1)
        {
            _slots[lastSlot].GetChild(1).gameObject.SetActive(false);
            _slots[selected].GetChild(1).gameObject.SetActive(true);
            lastSlot = selected;
        }


    }
    void ItemPickUp(PickUpInformation info)
    {
        int freeSlotIndex = -1;
        for (int i = 0; i < _slots.Length; i++) // Знаходження вільного слоту.
        {
            if (_slots[i].childCount < 3)
            {
                freeSlotIndex = i;
                break;
            }
            else
            {
                SlotItemInformation _slotInfo = _slots[i].GetComponent<SlotItemInformation>();
                if (_slotInfo.name == info.name)
                {
                    _amount[i]++;
                    Destroy(info.gameObject);
                    AmountUI(_amount[i], i);
                    return;

                }
            }
        }
        if (freeSlotIndex == -1) return;
        Image img = info.name switch
        {
            "apple" => _inventorySprites[0]
        };
        img = Instantiate(img) as Image;
        img.transform.parent = _slots[freeSlotIndex];
        _amount[freeSlotIndex]++;
        _slots[freeSlotIndex].GetComponent<SlotItemInformation>().name = info.name;
        img.transform.localPosition = new Vector3(0, 0, 0);
        Destroy(info.gameObject);
        


    }
    void AmountUI(int amount, int index) //Текст з кількістю однакових речей в одному слоті
    {
        if (amount != 1)
        {
            TMP_Text _text = _slots[index].GetChild(0).GetComponent<TMP_Text>();
            _text.text = $"{amount}";
           
            if (_text.text.ToCharArray().Length > 1)
            {
                if (moved) return;
                _text.fontSize /= 1.5f;
                _slots[index].GetChild(0).transform.position += new Vector3(-1.5f, 0);
                moved = true;
            }
            else
            {
                _text.fontSize = 18;
                if (moved)
                {
                    _slots[index].GetChild(0).transform.position -= new Vector3(-1.5f, 0);
                }
            }
        }
    }
}
