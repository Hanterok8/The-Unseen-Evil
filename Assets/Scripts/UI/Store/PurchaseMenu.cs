using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseMenu : MonoBehaviour
{
    private ItemData[] _itemDatum;

    private GameObject _buyMenu;

    private TMP_Text _priceText;
    private TMP_Text _itemText;
    private Image _itemImage;
    ItemCost _itemCost;

    private PlayerOrDemon _playerOrDemon;
    private void Awake()
    {
        BuyMenuInfo menuInfo = GameObject.FindGameObjectWithTag("Store_Menu").GetComponent<BuyMenuInfo>();
        _itemDatum = menuInfo.itemDatum;
        _priceText = menuInfo.priceText;
        _itemText = menuInfo.itemText;
        _itemImage = menuInfo.itemImage;
        _buyMenu = menuInfo.buyMenu;
        _itemCost = _priceText.GetComponent<ItemCost>();
        _buyMenu.SetActive(false);
    }
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerInstance");
        _playerOrDemon = player.GetComponent<PlayerOrDemon>();
    }
    public void OnItemClick()
    {
        int _id = GetComponent<ItemID>().id;
        if (_itemDatum[_id].isOnlyForResidents && _playerOrDemon.isDemon) return; 
        if(!_buyMenu.activeSelf) _buyMenu.SetActive(true);
        GameObject purchaseButton = _priceText.transform.parent.gameObject;
        purchaseButton.GetComponent<OnPurchase>().lastClickedButtonID = _id;
        purchaseButton.GetComponent<Button>().interactable = true;
        _itemCost.UpdatePrice(_itemDatum[_id].price);
        _priceText.text = $"{_itemDatum[_id].price}";
        _itemText.text = _itemDatum[_id].name;
        _itemImage.sprite = _itemDatum[_id].sprite;
    }
}
