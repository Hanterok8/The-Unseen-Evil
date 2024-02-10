using Photon.Pun;
using UnityEngine;

public class CrouchControlller : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _crouchSpeed = 2;
    public bool isCrouchFrozen;
    private PersonController _personController;
    private PhotonView _photonView;
    private int _state;
    private void Start()
    {
        isCrouchFrozen = false;
        _personController = GetComponent<PersonController>();
        _photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (_photonView == null) _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine && !isCrouchFrozen) CrouchInput();

    }
    private void CrouchInput()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _personController.enabled = false;
            _state = 11;
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * _crouchSpeed * Time.deltaTime);
                _state = 12;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * _crouchSpeed * Time.deltaTime);
                _state = 13;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * _crouchSpeed * Time.deltaTime);
                _state = 14;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * _crouchSpeed * Time.deltaTime);
                _state = 15;
            }
        }
        else
        {
            GetComponent<PersonController>().enabled = true;
            _state = 0;
        }
        ChangeCrouchAnimation(_state);
    }
    public void ChangeCrouchAnimation(int playerState)
    {
        _photonView.RPC(nameof(ChangeAnimationRPC), RpcTarget.All, playerState);
    }
    [PunRPC]
    private void ChangeAnimationRPC(int animationState)
    {
        _animator.SetInteger("State", animationState);
    }
}
