using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuInfo : MonoBehaviour
{
    [SerializeField] private  ItemData[] _itemDatum;

    [SerializeField] private GameObject _buyMenu;

    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _itemText;
    [SerializeField] private Image _itemImage;

    public ItemData[] itemDatum => this._itemDatum;
    public GameObject buyMenu => this._buyMenu;
    public TMP_Text priceText => this._priceText;
    public TMP_Text itemText => this._itemText;
    public Image itemImage => this._itemImage;


}
