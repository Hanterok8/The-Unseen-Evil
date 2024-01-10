using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RewardForGhost : MonoBehaviour
{
    CoinKeeper coin;
    public void Start()
    {
        CoinKeeper coin = GetComponent<CoinKeeper>();
    }
    public void GiveReward(int CountsOfCoins ) 
    {
        if (coin != null) 
        {
            coin.AddCoins(CountsOfCoins);
            Debug.Log("дало");
        }
        Destroy(gameObject);
    }
}
