using Photon.Pun;
using UnityEngine;
public class PlayerOrDemon : MonoBehaviour
{
    [SerializeField] public bool isDemon;
    private GameObject Demon;
    private GameObject DemonModel;
    private PhotonView photonView;
    private string demonNickname;
    private bool isChecked = false;
    private void Start()
    {
        GameObject livingPlayer;
        //livingPlayer = GetComponent<CurrentPlayer>().CurrentPlayerModel;
        //photonView = livingPlayer.GetComponent<PhotonView>();
        photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && !isChecked)
        {
            demonNickname = GetDemonNickname();
            InvokeRepeating(nameof(CheckIsDemonConnected), 0, 0.5f);
            isChecked = true;
        }
    }
    private void CheckIsDemonConnected()
    {
        GameObject[] playersInGame = GameObject.FindGameObjectsWithTag("PlayerInstance");
        if (playersInGame.Length == PlayerPrefs.GetInt("PlayerCount"))
        {
            FindDemonPlayerObject();
            CancelInvoke();
        }
    }
    private void FindDemonPlayerObject()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerInstance");
        foreach(GameObject player in players)
        {
            GameObject playerModel = player.GetComponent<CurrentPlayer>().CurrentPlayerModel;
            if (playerModel.GetComponent <PhotonView>().Owner.NickName == demonNickname)
            {
                Demon = player;
                DemonModel = playerModel;
                photonView.RPC(nameof(TurnOnDemonScripts), RpcTarget.All);
                break;
            }
        }
    }
    [PunRPC]
    private void TurnOnDemonScripts()
    {
        Demon.GetComponent<PlayerOrDemon>().isDemon = true;
        Demon.GetComponent<OnAimodipsisMode>().enabled = true;
        DemonModel.GetComponent<StaminaSettings>().isDemon = true;
        Demon.GetComponent<BloodlustSettings>().enabled = true;
    }
    private string GetDemonNickname()
    {
        return PlayerPrefs.GetString("Demon");
    }

}
