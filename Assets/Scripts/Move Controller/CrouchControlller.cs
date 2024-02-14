using Photon.Pun;
using UnityEngine;

public class CrouchControlller : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    public bool isCrouchFrozen;
    private CharacterController _charachterController;
    private PhotonView _photonView;
    private int _state;
    private void Start()
    {
        isCrouchFrozen = false;
        _charachterController = GetComponent<CharacterController>();
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
            _charachterController.enabled = false;
            _state = 1;
        }
        else
        {
            _charachterController.enabled = true;
            _state = 0;
        }
    }
}
