using Photon.Pun;
using UnityEngine;

public class MazeCloser : MonoBehaviour
{
    [SerializeField] private GameObject mazeCloserWall;
    [SerializeField] private GameObject mazeEnding;
    private GameObject Player;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().Owner.NickName == photonView.Owner.NickName)
            {
                Player = player;
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == Player && photonView.IsMine && Player.GetComponent<QuestSwitcher>().currentQuest.name == "Escape from the Mirage Maze")
        {
            mazeCloserWall.SetActive(true);
            mazeEnding.SetActive(true);
        }
    }
}
