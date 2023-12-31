using Photon.Pun;
using System.Collections;
using UnityEngine;

public class DemonAbilities : MonoBehaviour
{
    [SerializeField] private float distanceLimit;
    [SerializeField] private LayerMask aimLayer;
    [SerializeField] private ParticleSystem particlesAtKilling;
    private Camera mainCamera;
    private PhotonView photonView;
    private bool inCooldown;
    private GameObject playerParam;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        mainCamera = Camera.main;
        inCooldown = false;
    }

    private void Update()
    {
        if (!photonView) return;
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, distanceLimit, aimLayer))
        {
            if (Input.GetMouseButtonDown(0) && !inCooldown)
            {
                Debug.Log("Aimed on player");
                GameObject inhabitant = hit.collider.gameObject;
                KillInhabitant(5, inhabitant);
            }
        }
    }
    private void KillInhabitant(float timeToKill, GameObject inhabitant)
    {
        PersonController inhabitantController = inhabitant.GetComponent<PersonController>();
        inhabitantController.isFrozen = true;
        GetComponent<DemonController>().isFrozen = true;
        playerParam = inhabitant;
        photonView.RPC(nameof(ÑhangePositionRPC), RpcTarget.All);
        GameObject particleSystem = PhotonNetwork.Instantiate
            (particlesAtKilling.name, inhabitant.transform.position, Quaternion.Euler(-90,0,0));
        Health inhabitantHealth = inhabitant.GetComponent<Health>();
        Invoke(nameof(inhabitantHealth.KillPlayer), timeToKill);
        Destroy(particleSystem, timeToKill);
        StartCoroutine(ResetCooldown(3));
        GetComponent<DemonController>().isFrozen = false;

    }
    
    private IEnumerator ResetCooldown(int cooldownTime)
    {
        inCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        inCooldown = false;
    }

    [PunRPC]
    private void ÑhangePositionRPC()
    {
        playerParam.transform.localPosition += new Vector3(0f, 0.75f, 0f);
        playerParam.GetComponent<Rigidbody>().useGravity = true;
        playerParam.GetComponent<PersonController>().animator.SetInteger("State", 7);

    }
}
