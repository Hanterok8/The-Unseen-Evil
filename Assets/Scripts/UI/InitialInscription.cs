using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialInscription : MonoBehaviour
{
    [SerializeField] private GameObject residentInscription;
    [SerializeField] private GameObject demonInscription;

    private void Start()
    {
        PlayerOrDemon player = FindObjectOfType<PlayerOrDemon>();
        bool isDemon = player.isDemon;
        if(isDemon) demonInscription.SetActive(true);
        else residentInscription.SetActive(true);
    }
}
