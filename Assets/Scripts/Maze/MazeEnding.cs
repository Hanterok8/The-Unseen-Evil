using Photon.Pun;
using UnityEngine;

public class MazeEnding : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject playerModel;
    private PhotonView photonView;
    private GameObject gates;
    private void Start()
    {
        gates = GameObject.FindGameObjectWithTag("MazeGates");
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine) return;
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("PlayerInstance");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().Owner.NickName == photonView.Owner.NickName)
            {
                Player = player;
                playerModel = Player.GetComponent<CurrentPlayer>().CurrentPlayerModel;
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == playerModel && collider.GetComponent<PhotonView>().Owner.NickName == photonView.Owner.NickName && photonView.IsMine)
        {
            Debug.Log("ended maze");
            EndQuest();
        }
    }

    private void EndQuest()
    {
        Animator gaterAnimator = gates.GetComponent<Animator>();
        gaterAnimator.SetTrigger("CloseGates");
        gaterAnimator.ResetTrigger("OpenGates");
        QuestSwitcher questSwitcher = Player.GetComponent<QuestSwitcher>();
        questSwitcher.AddQuestStep(1);
        Debug.Log(questSwitcher + "Added quest step.");
        if(photonView.IsMine) Destroy(gameObject);
    }
}
