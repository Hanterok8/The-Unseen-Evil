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
        if(!photonView.IsMine) return;
        PhotonNetwork.Instantiate(spectatorPlayer.name, Vector3.zero, Quaternion.identity);
       // Invoke(nameof(DestroyPlayer), 1.5f);
        photonView.RPC(nameof(KickPlayerRPC), RpcTarget.All);
        PhotonNetwork.Destroy(gameObject);
    }
    

    [PunRPC]
    private void DestroyPlayerRPC()
    {
        Destroy(gameObject);
    }
    [PunRPC]
    private void KickPlayerRPC()
    {
        if (!photonView.IsMine) return;
        CurrentPlayer[] players = FindObjectsOfType<CurrentPlayer>();
        foreach (CurrentPlayer player in players)
        {
            if (player.CurrentPlayerModel == gameObject)
            {
                Destroy(player.gameObject);
                break;
            }
        }
        onTransformedToSpectator?.Invoke();

    }
}
