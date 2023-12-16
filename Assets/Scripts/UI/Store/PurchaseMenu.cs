using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseMenu : MonoBehaviour
{
    private ItemData[] _itemDatum;

    private GameObject _buyMenu;

    private TMP_Text _priceText;
    private TMP_Text _itemText;
    Image _itemImage;

    private void Awake()
    {
        BuyMenuInfo menuInfo = GameObject.FindGameObjectWithTag("Store_Menu").GetComponent<BuyMenuInfo>();
        _itemDatum = menuInfo.itemDatum;
        _priceText = menuInfo.priceText;
        _itemText = menuInfo.itemText;
        _itemImage = menuInfo.itemImage;
        _buyMenu = menuInfo.buyMenu;
        _buyMenu.SetActive(false);
    }
    public void OnItemClick()
    {
        Debug.Log("Item store clicked");
        if(!_buyMenu.activeSelf) _buyMenu.SetActive(true);
        int _id = GetComponent<ItemID>().id;
        _priceText.text = _itemDatum[_id].price;
        _itemText.text = _itemDatum[_id].name;
        _itemImage.sprite = _itemDatum[_id].sprite;
    }
}
