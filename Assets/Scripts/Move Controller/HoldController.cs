using Photon.Pun;
using UnityEngine;

public class HoldController : MonoBehaviour
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
        if (_photonView.IsMine) HoldInput();
    }
    private void HoldInput()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            _personController.ChangePlayerAnimation(30);
        }
    }
}