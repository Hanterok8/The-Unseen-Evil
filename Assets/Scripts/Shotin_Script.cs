using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shotin_Script : MonoBehaviour
{
    public float range = 100f;
    public GameObject shootOriginal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootOriginal.transform.position, shootOriginal.transform.forward, out hit, range))
        {
            Debug.DrawLine(shootOriginal.transform.position, hit.point, Color.red, 10f);
            Debug.Log("Столкновение с объектом: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Луч не столкнулся с объектом");
        }
    }

}
