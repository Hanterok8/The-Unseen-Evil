using UnityEngine;

public class ItemCost : MonoBehaviour
{
    public int price;
    public void UpdatePrice(int newPrice)
    {
        price = newPrice;
    }
}
