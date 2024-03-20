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
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform weaponEnd;
    private CharacterController _charachterController;
    private PhotonView _photonView;
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
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
            weapon.RaycastThrower = cam.transform;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _charachterController.SetAimingAnimation(false);
            LHoldTarget.transform.parent = _LHand.transform;
            weapon.RaycastThrower = weaponEnd;
        }
    }
    
}
