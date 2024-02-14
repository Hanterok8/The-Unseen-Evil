using Photon.Pun;
using System;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    [Header("Gun stats")]
    public int damage;
    public float timeBetweenShooting, range, reloadTime, timeBetweenShots;
    public int magazineSize;
    public bool allowButtonHold;
    [SerializeField] int bulletsLeft;
    int bulletsShot;

    bool shooting, readyToShoot, reloading;
    public float fireRate = 15f;
    private Camera fpsCam;
    [Header("Reference")]
    public RaycastHit rayHit;
    public GameObject impactEffect;
    [Header("Graphics")]
    public ParticleSystem muzzleFlash;
    public float impactForce = 30f;
    GameObject impactGO;    
    private PhotonView photonView;
    private CurrentPlayer player;
    private void Awake()
    {
        player = FindObjectOfType<CurrentPlayer>();
        photonView = player.CurrentPlayerModel.GetComponent<PhotonView>();
        fpsCam = Camera.main;
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    public void Update()
    {
        if (!photonView.IsMine) return;
        if (Input.GetButtonDown("Fire1")&&Time.time>=timeBetweenShooting) 
        {
            MyInput();
            if ( bulletsLeft == 0 && !reloading) Reload();
        }
        else if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }
    }
   
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 1;
            timeBetweenShooting = Time.time + 1f / fireRate;
            if (bulletsShot > 0 && bulletsLeft > 0 && !reloading)
                Invoke(nameof(Shoot), timeBetweenShots);
        }
    }
    void Shoot()
    {
        //muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            RewardForGhost ghost = hit.transform.GetComponent<RewardForGhost>();
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke(nameof(ResetShot), timeBetweenShooting);

        impactGO = PhotonNetwork.Instantiate(impactEffect.name, hit.point, Quaternion.LookRotation(hit.normal));
        Invoke(nameof(DestroyProjectile), 2f);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke(nameof(Shoot), timeBetweenShots);
    }

    private void DestroyProjectile()
    {
        PhotonNetwork.Destroy(impactGO);
    }

    void Reload() 
    {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
    private void ResetShot()    
    {
        readyToShoot = true;
    }
}

