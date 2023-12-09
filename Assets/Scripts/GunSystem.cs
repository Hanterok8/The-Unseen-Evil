using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem_Yarik : MonoBehaviour
{
    public int damage;
    public float timeBeetwenShoting, spread, range, reloadTime, timeBeetwenShots;
    public int magazinSize, bulletsPerTap;
    public bool alowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera mainCamera;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public GameObject muzzleFlash, bulletHoleGraphics; 

    private void Awake()
    {
        bulletsLeft = magazinSize;
        readyToShoot = true; 
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();   
    }
    private void MyInput()
    {
        if (alowButtonHold)shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazinSize && !reloading) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished() 
    {
        bulletsLeft = magazinSize;
        reloading = false;
    }
    private void ResetShoot() 
    {
        readyToShoot = true;
    }
    private void Shoot() 
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //if use rigidbody  
        // if(rigidbody.velocity.magnitude>0)
        //spread=spread*1.5f;
        //else spread = "normal spread";
        Vector3 direction= mainCamera.transform.forward+new Vector3(x, y, 0);

        if (Physics.Raycast(mainCamera.transform.position, direction, out rayHit, range, whatIsEnemy)) 
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                //rayHit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        Instantiate(bulletHoleGraphics, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShoot", timeBeetwenShoting);

        if(bulletsShot>0&&bulletsLeft>0)Invoke("Shoot", timeBeetwenShots);
    }
}
