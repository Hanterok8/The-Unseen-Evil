using TMPro;
using UnityEngine;

public class CoinKeeper : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsOnDisplay;
    private QuestSwitcher questSwitcher;
    public int coins = 0;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerInstance");
        questSwitcher = player.GetComponent<QuestSwitcher>();
        questSwitcher.onReceivedCoinsByQuest += AddCoins;
    }
    private void OnEnable()
    {
        //questSwitcher.onReceivedCoinsByQuest += AddCoins;
    }
    private void OnDisable()
    {
        questSwitcher.onReceivedCoinsByQuest -= AddCoins;
    }
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
