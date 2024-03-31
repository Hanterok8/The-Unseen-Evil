using System;
using Photon.Pun;
using UnityEngine;

public class TouchingAnomaly : MonoBehaviour
{
    public Action<Collider> onAnomalyTouched;
    private PhotonView _photonView;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Anomaly") && _photonView.IsMine && collider.gameObject.GetComponent<PhotonView>().Owner.NickName == _photonView.Owner.NickName)
        {
            onAnomalyTouched?.Invoke(collider);
            Destroy(collider);
        }
    }
}
