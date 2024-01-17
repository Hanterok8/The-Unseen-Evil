using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemsGameObjects;
    public GameObject[] ItemGameObjects => _itemsGameObjects;
}
