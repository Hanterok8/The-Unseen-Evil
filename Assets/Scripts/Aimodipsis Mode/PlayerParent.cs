using Photon.Pun;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Transform parentTransform;
    public void SetNewParent(Transform newParent)
    {
        photonView = GetComponent<PhotonView>();
        parentTransform = newParent;

        photonView.RPC(nameof(SetParentRPC), RpcTarget.All);
    }
    [PunRPC]
    private void SetParentRPC()
    {
        transform.SetParent(Object.FindFirstObjectByType<GameManager>().spawnedPlayer.transform);
    }
}
