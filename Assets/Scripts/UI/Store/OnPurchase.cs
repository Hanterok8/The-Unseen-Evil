using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnPurchase : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    public int _lastClickedButtonID;
    public void OnPurchasingItem()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerInstance");
        ItemControl itemControl = player.GetComponent<ItemControl>();
        itemControl.ItemReceive(_itemName.text);

        ItemID[] itemButtons = Object.FindObjectsOfType<ItemID>();
        for(int i = 0;i < itemButtons.Length; i++)
        {
            if (itemButtons[i].id == _lastClickedButtonID)
            {
                itemButtons[i].GetComponent<Button>().interactable = false;
                break;
            }
        }
        GetComponent<Button>().interactable = false;
    }
}
