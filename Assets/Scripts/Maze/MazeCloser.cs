using Photon.Pun;
using UnityEngine;

public class MazeCloser : MonoBehaviour
{
    [SerializeField] private GameObject mazeCloserWall;
    [SerializeField] private GameObject mazeEnding;
    private GameObject Player;
    private GameObject playerModel;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
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
        if (collider.gameObject == playerModel && photonView.IsMine && Player.GetComponent<QuestSwitcher>().currentQuest.name == "Escape from the Mirage Maze")
        {
            mazeCloserWall.SetActive(true);
            mazeEnding.SetActive(true);
        }
    }
}
