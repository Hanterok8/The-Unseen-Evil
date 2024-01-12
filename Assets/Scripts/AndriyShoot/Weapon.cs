using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weapon : MonoBehaviour
{
    private float Damage = 20f;
    private float fireRate = 5f;
    private float Range = 100f;
    private float Forse = 155f;
    public float maxAmmo = 120f;
    private float nextFire = 0f;
    public float ammoInClip = 30;
    private float ReloadTime = 1f;
    private bool CanShoot = true;


    public GameObject muzleFlash;
    public GameObject hitEffect;

    public Transform bulletSpawn;

    public AudioClip shotSFX;
    public AudioClip reloadSFX;

    public AudioSource AudioSource;
    public AudioSource audioReload;

    private Camera Cam;

    private TMP_Text Ammo;
    private void Start()
    {
        Cam = Camera.main;
        Ammo = GameObject.FindGameObjectWithTag("AmmoCount").GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && ammoInClip > 0 && Time.time > nextFire && CanShoot)
        {
            nextFire = Time.time + 1f / fireRate;
            ammoInClip -= 1;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && maxAmmo > 0)
        {
            StartCoroutine(ReloadDelay());
            Reload();
        }
        Ammo.text = ammoInClip + " / " + maxAmmo;
    }
    private IEnumerator ReloadDelay()
    {
        CanShoot = false;
        yield return new WaitForSeconds(ReloadTime);
        CanShoot = true;
    }
    void Shoot()
    {
        AudioSource.PlayOneShot(shotSFX);
        Instantiate(muzleFlash, bulletSpawn.position, bulletSpawn.rotation);

        RaycastHit Hit;

        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out Hit, Range))
        {
            GameObject ImpactGO = Instantiate(hitEffect, Hit.point, Quaternion.LookRotation(Hit.normal));
            Destroy(ImpactGO, 2f);

            if (Hit.rigidbody != null)
            {
                Hit.rigidbody.AddForce(-Hit.normal * Forse);
            }
        }
    }
    void Reload()
    {
        float Reason = 30f - ammoInClip;
        if (ammoInClip < 30)
        {
            audioReload.PlayOneShot(reloadSFX);
        }
        if (maxAmmo >= Reason)
        {
            maxAmmo = maxAmmo - Reason;
            ammoInClip = 30f;
        }
        else
        {
            ammoInClip = ammoInClip + maxAmmo;
            maxAmmo = 0;
        }
    }
    private void OnDisable()
    {
        Ammo.text = "";
    } 
}
