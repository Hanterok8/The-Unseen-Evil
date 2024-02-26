using Photon.Pun;
using System;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    private PhotonView photonView;
    [SerializeField] private GameObject spectatorPlayer;
    public Action onTransformedToSpectator;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    public void LookAtDemon(Transform demon)
    {
        transform.LookAt(demon);
    }
    public void KickPlayer()
    {
        photonView.RPC(nameof(KickPlayerRPC), RpcTarget.All);
    }
    [PunRPC]
    internal void KickPlayerRPC()
    {
        if (!photonView.IsMine) return;
        CurrentPlayer[] players = FindObjectsOfType<CurrentPlayer>();
        GameObject spectator = Instantiate(spectatorPlayer);
        foreach (CurrentPlayer player in players)
        {
            if (player.CurrentPlayerModel == gameObject)
            {
                //Destroy(player.gameObject);
                player.GetComponent<CurrentPlayer>().CurrentPlayerModel = spectator;
                break;
            }
        }
        Destroy(gameObject);
        onTransformedToSpectator?.Invoke();
        //PhotonNetwork.Disconnect();

    }
}
