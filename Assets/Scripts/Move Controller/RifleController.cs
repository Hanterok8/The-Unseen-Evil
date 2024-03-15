using Photon.Pun;
using UnityEngine;

public class RifleController : MonoBehaviour
{
    [SerializeField] private GameObject LHoldTarget;
    [SerializeField] private GameObject _RHand;
    [SerializeField] private GameObject _LHand;
    [SerializeField] private GameObject _WeaponTarget;
    [SerializeField] private GameObject _LForeArm;
    [SerializeField] private GameObject player;
    private CharacterController _charachterController;
    private PhotonView _photonView;
    private void Start()
    {
        _photonView = player.GetComponent<PhotonView>();
        _charachterController = player.GetComponent<CharacterController>();
        
        _LForeArm.transform.localRotation = new Quaternion(0,0,0,0);
    }
    private void Update()
    {
        if (_photonView == null) _photonView = player.GetComponent<PhotonView>();
        if (_photonView.IsMine) RifleInput();

    }
    private void RifleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _charachterController.SetAimingAnimation(true);
            LHoldTarget.transform.parent = _RHand.transform;
            LHoldTarget.transform.position = _WeaponTarget.transform.position;
            LHoldTarget.transform.rotation = _WeaponTarget.transform.rotation;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _charachterController.SetAimingAnimation(false);
            LHoldTarget.transform.parent = _LHand.transform;
        }
    }
    
}
