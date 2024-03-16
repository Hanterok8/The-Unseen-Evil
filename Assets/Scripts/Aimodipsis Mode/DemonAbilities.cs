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
    private Animator playerAnimator;
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
        if (!photonView.IsMine) return;
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, distanceLimit, aimLayer))
        {
            if (Input.GetMouseButtonDown(0) && !inCooldown)
            {
                Debug.Log("Aimed on player");
                GameObject resident = hit.collider.gameObject;
                currentPlayerParam = resident;

                KillInhabitant(5);
            }
        }
    }
    public void KillInhabitant(int timeToKill)
    {
        photonView.RPC(nameof(KillPlayerRPC), RpcTarget.All, timeToKill, currentPlayerParam.GetComponent<PhotonView>().Owner.NickName);
    }

    [PunRPC]
    private void KillPlayerRPC(int time, string playerNickname)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject killedPlayer = null;
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().Owner.NickName == playerNickname)
            {
                killedPlayer = player;
                break;
            }
        }
        PlayerSetter playerSettings = killedPlayer.GetComponent<PlayerSetter>();
        //residentController.SetNewFrozenValue(true);
        GetComponent<DemonController>().isDemonFrozen = true;
        killedPlayer.transform.localPosition += new Vector3(0f, 0.75f, 0f);
        killedPlayer.GetComponent<Rigidbody>().isKinematic = true;
        playerSettings.LookAtDemon(transform);
        playerSettings.KickPlayer();
        GameObject particleSystem = PhotonNetwork.Instantiate
            (particlesAtKilling.name, killedPlayer.transform.position, Quaternion.Euler(-90, 0, 0));
        Invoke(nameof(DeleteParticles), time);
        StartCoroutine(ResetCooldown(time));
        Invoke(nameof(UnfreezeDemon), time);
    }
    [PunRPC]
    private void ChangePlayerAnimationRPC(int animationState)
    {
        playerAnimator.SetInteger("State", animationState);
    }
    private void DeleteParticles()
    {
        photonView.RPC(nameof(DestroyParticlesRPC), RpcTarget.All);
    }
    [PunRPC]
    private void DestroyParticlesRPC()
    {
        Destroy(currentParticlesParam);
    }
    private void UnfreezeDemon() => GetComponent<DemonController>().isDemonFrozen = false;

    private IEnumerator ResetCooldown(int cooldownTime)
    {
        inCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        inCooldown = false;
    }

}
