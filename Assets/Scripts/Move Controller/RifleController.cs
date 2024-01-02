using Photon.Pun;
using UnityEngine;

public class RifleController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _animator;

    private PhotonView _photonView;
    private int _state;
    private void Start()
=>      _photonView = transform.parent.GetComponent<PhotonView>();
    private void Update()
    {
        if (_photonView == null) _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine) RifleInput();

    }
    private void RifleInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _state = 20;
        }
        if (Input.GetKey(KeyCode.T))
        {
            _state = 21;
        }
        
    }
}
