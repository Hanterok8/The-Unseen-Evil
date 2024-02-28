using Photon.Pun;
using UnityEngine;

public class RifleController : MonoBehaviour
{
    private CharacterController _charachterController;
    [SerializeField] private GameObject LHoldTarget;
    [SerializeField] private GameObject _RHand;
    [SerializeField] private GameObject _LHand;
    [SerializeField] private GameObject _WeaponTarget;
    [SerializeField] private GameObject _Player;
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
        if (Input.GetMouseButtonDown(1))
        {
            _charachterController._playerAnimator.SetBool("isAiming", true);
            LHoldTarget.transform.parent = _RHand.transform;
            LHoldTarget.transform.position = _WeaponTarget.transform.position;
            LHoldTarget.transform.rotation = _WeaponTarget.transform.rotation;
            _Player.GetComponent<CameraController>().enabled = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _charachterController._playerAnimator.SetBool("isAiming", false);
            LHoldTarget.transform.parent = _LHand.transform;
            LHoldTarget.transform.position = new Vector3(0, 0, 0);
            _Player.GetComponent<CameraController>().enabled = true;
        }
    }
}
