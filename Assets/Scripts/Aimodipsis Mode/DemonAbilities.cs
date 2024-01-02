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

    private GameObject currentPlayerParam;
    private GameObject currentParticlesParam;
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
                currentPlayerParam = inhabitant;
                KillInhabitant(5);
            }
        }
    }
    public void KillInhabitant(int timeToKill)
    {
        photonView.RPC(nameof(KillPlayerRPC), RpcTarget.All, timeToKill);

    }

    private void UnfreezeDemon() => GetComponent<DemonController>().isDemonFrozen = false;
    private void DeleteParticles()
    {
        PhotonNetwork.Destroy(currentParticlesParam);
    }
    private IEnumerator ResetCooldown(int cooldownTime)
    {
        inCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        inCooldown = false;
    }

    [PunRPC]
    private void KillPlayerRPC(int time)
    {
        PersonController inhabitantController = currentPlayerParam.GetComponent<PersonController>();
        inhabitantController.SetNewFrozenValue(true);
        GetComponent<DemonController>().isDemonFrozen = true;
        currentPlayerParam.transform.localPosition += new Vector3(0f, 0.75f, 0f);
        inhabitantController.SetKinematicModeForRigidbody();
        currentPlayerParam.GetComponent<PersonController>().ChangePlayerAnimation(7);
        GameObject particleSystem = PhotonNetwork.Instantiate
            (particlesAtKilling.name, currentPlayerParam.transform.position, Quaternion.Euler(-90, 0, 0));
        currentParticlesParam = particleSystem.gameObject;
        Health inhabitantHealth = currentPlayerParam.GetComponent<Health>();
        //Invoke(nameof(inhabitantHealth.KillPlayer), timeToKill);
        Invoke(nameof(DeleteParticles), time);
        StartCoroutine(ResetCooldown(time));
        Invoke(nameof(UnfreezeDemon), time);
    }

}
