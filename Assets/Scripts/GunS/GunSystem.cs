using UnityEngine;

public class GunSystem : MonoBehaviour
{
    [Header("Gun stats")]
    public int damage;
    public float timeBetweenShooting, range, reloadTime, timeBetweenShots;
    public int magazineSize;
    public bool allowButtonHold;
    [SerializeField]int bulletsLeft;
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

    public void Update()
    {
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
    private void Awake()
    {
        fpsCam = Camera.main;
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void MyInput()
    {
        //if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        //{
        //    Debug.Log("Reload button pressed");
        //}
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 1;
            timeBetweenShooting = Time.time + 1f / fireRate;
            if (bulletsShot > 0 && bulletsLeft > 0 && !reloading)
                Invoke("Shoot", timeBetweenShots);
        }
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

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 2f);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    void Reload() 
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
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

