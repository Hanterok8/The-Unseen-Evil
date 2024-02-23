using Photon.Pun;
using UnityEngine;
public class PlayerOrDemon : MonoBehaviour
{
    [SerializeField] public bool isDemon;
    //private GameObject Demon;
    private PhotonView photonView;
    //private string demonNickname;
    //private bool isChecked = false;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine && PhotonNetwork.DemonName == photonView.Owner.NickName)    
        {
            isDemon = true;
            GetComponent<OnAimodipsisMode>().enabled = true;
            GetComponent<CurrentPlayer>().CurrentPlayerModel.GetComponent<StaminaSettings>().isDemon = true;
            GetComponent<BloodlustSettings>().enabled = true;
        }
    }
    //private void Update()
    //{
    //    if (PhotonNetwork.IsMasterClient && !isChecked)
    //    {
    //        demonNickname = GetDemonNickname();
    //        InvokeRepeating(nameof(CheckIsDemonConnected), 0, 0.5f);
    //        isChecked = true;
    //    }
    //}
    //private void CheckIsDemonConnected()
    //{
    //    GameObject[] playersInGame = GameObject.FindGameObjectsWithTag("PlayerInstance");
    //    int numberOfRequiredPlayers = PlayerPrefs.GetInt("PlayerCount");
    //    if (playersInGame.Length == numberOfRequiredPlayers)
    //    {
    //        FindDemonPlayerObject();
    //        CancelInvoke();
    //    }
    //}
    //private void FindDemonPlayerObject()
    //{
    //    GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerInstance");
    //    foreach(GameObject player in players)
    //    {
    //        GameObject playerModel = player.GetComponent<CurrentPlayer>().CurrentPlayerModel;
    //        if (playerModel.GetComponent<PhotonView>().Owner.NickName == demonNickname)
    //        {
    //            Demon = player;
    //            photonView.RPC(nameof(TurnOnDemonScriptsRPC), RpcTarget.All);
    //            break;
    //        }
    //    }
    //}
    //[PunRPC]
    //private void TurnOnDemonScriptsRPC()
    //{
    //    Demon.GetComponent<PlayerOrDemon>().TurnOnMyDemonScripts();
    //}
    //private string GetDemonNickname()
    //{
    //    return PlayerPrefs.GetString("Demon");
    //}
    //public void TurnOnMyDemonScripts()
    //{
    //    isDemon = true;
    //    GetComponent<OnAimodipsisMode>().enabled = true;
    //    GetComponent<CurrentPlayer>().CurrentPlayerModel.GetComponent<StaminaSettings>().isDemon = true;
    //    GetComponent<BloodlustSettings>().enabled = true;
    //}

}
