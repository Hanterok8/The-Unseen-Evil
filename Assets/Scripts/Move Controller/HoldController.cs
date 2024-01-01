using Photon.Pun;
using UnityEngine;

public class HoldController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _animator;

    private PhotonView _photonView;
    private int _state;
    private void Start()
=> _photonView = transform.parent.GetComponent<PhotonView>();
    private void Update()
    {
        if (_photonView == null) _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine) HoldInput();

    }
    private void HoldInput()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            _state = 30;
        }
        _animator.SetInteger("State", _state);
    }
}
