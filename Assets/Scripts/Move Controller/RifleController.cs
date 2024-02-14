using Photon.Pun;
using UnityEngine;

public class RifleController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera AIMCamera;
    private CharacterController _charachterController;
    private PhotonView _photonView;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _charachterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (_photonView == null) _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine) RifleInput();

    }
    private void RifleInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _charachterController._playerAnimator.SetBool("Equp", true);
        }
        if (Input.GetKey(KeyCode.T))
        {
            _charachterController._playerAnimator.SetBool("Equp", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            _charachterController._playerAnimator.SetBool("isAiming", true);
            mainCamera.enabled = false;
            AIMCamera.enabled = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _charachterController._playerAnimator.SetBool("isAiming", false);
            mainCamera.enabled = true;
            AIMCamera.enabled = false;
        }
    }
}
