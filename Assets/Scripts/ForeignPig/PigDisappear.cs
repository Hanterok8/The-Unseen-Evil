using Photon.Pun;
using UnityEngine;

public class PigDisappear : MonoBehaviour
{
    [SerializeField] private GameObject uglyFace;
    [SerializeField] private GameObject normalFace;
    private PhotonView photonView;
    private PigQuestEnd pigQuestEnd;
    void Start()
    {
        GameObject Player;
        GameObject[] players;
        photonView = GetComponent<PhotonView>();
        pigQuestEnd = GetPigQuestEnd();
        players = GameObject.FindGameObjectsWithTag("PlayerInstance");
        pigQuestEnd.onQuestEnded += Disappear;
    }
    private void OnDisable()
    {
        pigQuestEnd.onQuestEnded -= Disappear;
    }
    private PigQuestEnd GetPigQuestEnd()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("PlayerInstance");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().Owner.NickName == photonView.Owner.NickName)
            {
                return player.GetComponent<PigQuestEnd>();
            }
        }
        return null;
    }
    private void Disappear()
    {
        GameObject player = pigQuestEnd.gameObject;
        GameObject playerModel = player.GetComponent<CurrentPlayer>().CurrentPlayerModel;
        transform.LookAt(playerModel.transform);
        uglyFace.SetActive(true);
        normalFace.SetActive(false);
        Destroy(gameObject, 1);
    }
}
