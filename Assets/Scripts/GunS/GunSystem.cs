using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;
    public float fireRate = 15f;
    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public GameObject impactEffect;
    //Graphics
    public ParticleSystem muzzleFlash;
    public float impactForce = 30f;

    public void Update()
    {
        if (Input.GetButtonDown("Fire1")&&Time.time>=timeBetweenShooting) 
        {
            timeBetweenShooting = Time.time+1f/fireRate;
            Shoot();
        }
        void Shoot() 
        {
            //muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) 
            {
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null) 
                {
                    target.TakeDamage(damage);
                }
                if (hit.rigidbody != null) 
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
            GameObject impacGO =  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impacGO, 2f);
        }
    }
}