using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnPurchase : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private ItemCost _coinsPrice;
    public int lastClickedButtonID;
    public void OnPurchasingItem()
    {
        CoinKeeper coinKeeper = FindObjectOfType<CoinKeeper>();
        if (_coinsPrice.price > coinKeeper.coins) return;
        GameObject player = GameObject.FindGameObjectWithTag("PlayerInstance");
        ItemControl itemControl = player.GetComponent<ItemControl>();
        itemControl.ReceiveItem(_itemName.text);
        coinKeeper.SubtractCoins(_coinsPrice.price);
        ItemID[] itemButtons = FindObjectsOfType<ItemID>();
        for(int i = 0;i < itemButtons.Length; i++)
        {
            if (itemButtons[i].id == lastClickedButtonID)
            {
                itemButtons[i].GetComponent<Button>().interactable = false;
                break;
            }
        }
        GetComponent<Button>().interactable = false;
    }
}
