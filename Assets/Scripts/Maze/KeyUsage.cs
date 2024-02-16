using Photon.Pun;
using UnityEngine;

public class KeyUsage : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameObject playerParent;
    private GameObject Player;
    [SerializeField] LayerMask layer;
    private const int DISTANCE = 155;
    private GameObject gates;
    private ItemControl itemControl;
    private PhotonView photonView;
    
    private void Start()
    {
        gates = GameObject.FindGameObjectWithTag("MazeGates");
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("PlayerInstance");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<CurrentPlayer>().CurrentPlayerModel == playerParent)
            {
                Player = player;
                break;
            }
        }
        itemControl = Player.GetComponent<ItemControl>();
        photonView = Player.GetComponent<PhotonView>();
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, DISTANCE, layer) && Input.GetMouseButtonDown(0) && photonView.IsMine)
        {
            OpenGates();   
        }
    }
    private void OpenGates()
    {
        if (itemControl._slots[itemControl.selected].GetComponent<SlotItemInformation>().name != "Maze key")
            return;
        gates.GetComponent<Animator>().SetTrigger("OpenGates");
        itemControl.TakeAwayItem();
    }
}
