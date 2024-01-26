using Photon.Pun;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PredictDemonPosition : MonoBehaviour
{
    [SerializeField] private GameObject particlesEffect;
    [SerializeField] private LayerMask layerWithDemon;
    [SerializeField] private GameObject myParent;
    private GameObject Player;
    private const int MAX_DISTANCE = 30;
    private Collider[] demonAround;
    private bool isUsingAbility = false;
    private int secondsLeft;
    private PhotonView photonView;
    private GameObject currentParticles;
    private void Start()
    {
        GameObject[] playersInGame = GameObject.FindGameObjectsWithTag("PlayerInstance");
        foreach (GameObject player in playersInGame)
        {
            if (player.GetComponent<CurrentPlayer>().CurrentPlayerModel == myParent)
            {
                Player = player;
            }
        }
        photonView = myParent.GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (!photonView.IsMine || !transform.GetChild(0).gameObject.activeSelf) return;
        if (Input.GetMouseButtonDown(0) && !isUsingAbility)
        {
            Debug.Log("tapped");
            ItemControl itemControl = Player.GetComponent<ItemControl>();
            itemControl.TakeAwayItem(itemControl.selected);
            StartCoroutine(UseAbility());
            isUsingAbility = true;
        }
        if (isUsingAbility && secondsLeft >= 0)
        {
            FindDemonsInRadius();
        }

    }

    private IEnumerator UseAbility()
    {
        secondsLeft = 20;
        currentParticles = Instantiate(particlesEffect);
        currentParticles.GetComponent<EffectFollow>().player = myParent.transform;
        while (secondsLeft >= -1)
        {
            yield return new WaitForSeconds(1);
            secondsLeft--;
        }
        Destroy(currentParticles);
        isUsingAbility = false;
        enabled = false;
    }
    private void FindDemonsInRadius()
    {
        demonAround = Physics.OverlapSphere(myParent.transform.position, MAX_DISTANCE, layerWithDemon);
        if (demonAround.Length > 0) CircleDemons();
        else UncircleDemon();
    }
    private void CircleDemons()
    {
        Outline demonOutline = demonAround[0].GetComponent<Outline>();
        demonOutline.enabled = true;
    }
    private void UncircleDemon()
    {
        Outline demonOutline = FindObjectOfType<Outline>();
        if(demonOutline) demonOutline.enabled = false;
    }
    private void OnDestroy()
    {
        Destroy(currentParticles?.gameObject);
        UncircleDemon();
    }
}
