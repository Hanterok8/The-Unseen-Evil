using Photon.Pun;
using UnityEngine;

public class RifleController : MonoBehaviour
{
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
        if (Input.GetKey(KeyCode.T))
        {
            _personController.ChangePlayerAnimation(21);
        }

    }
}
