using TMPro;
using UnityEngine;

public class CoinKeeper : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsOnDisplay;
    public int coins = 0;

    public void AddCoins(int numberOfCoins)
    {
        coins += numberOfCoins;
        UpdateCoinsNumberOnDisplay();
    }


    public void SubtractCoins(int numberOfCoins)
    {
        coins -= numberOfCoins;
        UpdateCoinsNumberOnDisplay();
    }
    private void UpdateCoinsNumberOnDisplay()
    {
        coinsOnDisplay.text = $"{coins}";
    }

}
