using Photon.Pun;
using UnityEngine;

public class HoldController : MonoBehaviour
{
    private CharacterController characterController;

    private PhotonView _photonView;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (_photonView.IsMine) HoldInput();
    }
    private void HoldInput()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            characterController._playerAnimator.SetBool("Hold", true);
        }
    }
}