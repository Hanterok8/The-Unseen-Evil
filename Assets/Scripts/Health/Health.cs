using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    private void Start()
=> health = 100;
    public void KillPlayer()
    {
        health = 0; 
        Debug.Log("killed player");
    }
    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage));
        }
        health -= damage;
    }
}
