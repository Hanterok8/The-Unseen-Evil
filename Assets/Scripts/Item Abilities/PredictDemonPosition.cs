using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PredictDemonPosition : MonoBehaviour
{
    [SerializeField] private GameObject particlesEffect;
    [SerializeField] private LayerMask layerWithDemon;
    [SerializeField] private GameObject myParent;
    private GameObject Player;
    private const int RADIUS = 360;
    private const int MAX_DISTANCE = 30;
    private Collider[] demonAround;
    private bool isUsingAbility = false;
    private int secondsLeft;
    private PhotonView photonView;
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
        if (!photonView.IsMine || transform.GetChild(0) == null) return;
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
        GameObject particles = Instantiate(particlesEffect);
        particles.GetComponent<EffectFollow>().player = myParent.transform;
        while (secondsLeft >= -1)
        {
            yield return new WaitForSeconds(1);
            secondsLeft--;
        }
        Destroy(particles);
        isUsingAbility = false;
        enabled = false;
    }
    private void FindDemonsInRadius()
    {
        demonAround = Physics.OverlapSphere(myParent.transform.position, RADIUS, layerWithDemon);
        if (demonAround.Length > 0) CircleDemons();
    }
    private void CircleDemons()
    {
        Outline demonOutline = demonAround[0].GetComponent<Outline>();
        if (Vector3.Distance(myParent.transform.position, demonAround[0].transform.position) < MAX_DISTANCE)
        {
            demonOutline.enabled = true;
        }
        else
        {
           demonOutline.enabled = false;
        }
    }
}
