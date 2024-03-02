using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private QuestSwitcher questSwitcher;
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
    private PhotonView photonView;
    private void Start()
    {
        GameObject mainPlayer = GetMainPlayer();
        photonView = mainPlayer.GetComponent<PhotonView>();
        questSwitcher = mainPlayer.GetComponent<QuestSwitcher>();
        Cam = Camera.main;
        Ammo = GameObject.FindGameObjectWithTag("AmmoCount").GetComponent<TMP_Text>();
    }
    private GameObject GetMainPlayer()
    {
        CurrentPlayer[] currentPlayers = FindObjectsOfType<CurrentPlayer>();
        foreach (CurrentPlayer currentPlayer in currentPlayers)
        {
            if (currentPlayer.CurrentPlayerModel == Player)
            {
                return currentPlayer.gameObject;
            }
        }
        return null;
    }
    void Update()
    {
        if (!photonView.IsMine) return;
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
        PhotonNetwork.Instantiate(muzleFlash.name, bulletSpawn.position, bulletSpawn.rotation);

        RaycastHit Hit;

        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out Hit, Range))
        {
            photonView.RPC(nameof(InstantiantEffect), RpcTarget.All, Hit);
            RewardForGhost ghost = Hit.transform.GetComponent<RewardForGhost>();
            if (ghost != null && questSwitcher.currentQuest.name == "Maruntian Soul Catcher")
            {
                questSwitcher.AddQuestStep(1);
            }
            if (Hit.rigidbody != null)
            {
                Hit.rigidbody.AddForce(-Hit.normal * Forse);
            }
        }
    }
    [PunRPC]
    private void InstantiantEffect(RaycastHit Hit)
    {
        GameObject ImpactGO = Instantiate(hitEffect, Hit.point, Quaternion.LookRotation(Hit.normal));
        Destroy(ImpactGO, 2f);
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
