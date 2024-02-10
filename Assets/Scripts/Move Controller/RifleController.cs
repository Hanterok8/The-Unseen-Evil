using Photon.Pun;
using UnityEngine;

public class RifleController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera AIMCamera;
    private PersonController _personController;
    private PhotonView _photonView;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _personController = GetComponent<PersonController>();
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
            _personController.ChangePlayerAnimation(20);
        }
        if (Input.GetMouseButtonDown(1))
        {
            _personController.ChangePlayerAnimation(22);
            mainCamera.enabled = false;
            AIMCamera.enabled = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _personController.ChangePlayerAnimation(21);
            mainCamera.enabled = true;
            AIMCamera.enabled = false;
        }
        if (Input.GetKey(KeyCode.T))
        {
            _personController.ChangePlayerAnimation(21);
        }
    }
}
