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
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        mainCamera = Camera.main;
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
                GameObject inhabitant = hit.collider.gameObject;
                KillInhabitant(inhabitant, 5);
            }
        }
    }

    private void KillInhabitant(GameObject inhabitant, float timeToKill)
    {
        PersonController inhabitantController = inhabitant.GetComponent<PersonController>();
        inhabitantController.isFrozen = true;
        inhabitantController.state = 7;
        inhabitant.transform.parent = transform;
        inhabitant.transform.localPosition += new Vector3(0f, 0.75f, 0f);
        GameObject particleSystem = PhotonNetwork.Instantiate(particlesAtKilling.name, inhabitant.transform.position, Quaternion.identity);
        particleSystem.transform.parent = inhabitant.transform;
        Health inhabitantHealth = inhabitant.GetComponent<Health>();
        Invoke(nameof(inhabitantHealth.KillPlayer), timeToKill);
        Destroy(particleSystem, timeToKill);
        ResetCooldown(30);
    }
    private IEnumerator ResetCooldown(int cooldownTime)
    {
        inCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        inCooldown = false;
    }
}
