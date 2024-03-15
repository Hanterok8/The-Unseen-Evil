using System.Collections.Generic;
using Photon.Pun;
using Unity.Netcode;
using UnityEngine;

public class FootSteep : MonoBehaviour
{
    [SerializeField] public AudioClip[] _footSound;
    [SerializeField] public AudioSource _footSourse;
    private int _randomSound;
    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        _footSound = Resources.LoadAll<AudioClip>("FootSound");
    }
    private void Play()
    {
        photonView.RPC(nameof(PlayInRPC), RpcTarget.All);
    }
    [PunRPC]
    private void PlayInRPC()
    {
        _randomSound = Random.Range(0, 3);
        _footSourse.PlayOneShot(_footSound[_randomSound]);
    }
}
