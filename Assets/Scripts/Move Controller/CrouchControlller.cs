using Photon.Pun;
using UnityEngine;

public class CrouchControlller : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _crouchSpeed = 2;

    private PhotonView _photonView;
    private int _state;
    private void Start()
=>      _photonView = transform.parent.GetComponent<PhotonView>();
    private void Update()
    {
        if (_photonView.IsMine) CrouchInput();
        
    }
    private void CrouchInput()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _player.GetComponent<PersonController>().enabled = false;
            _state = 11;
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * _crouchSpeed * Time.deltaTime);
                _state = 12;
            }
            if(Input.GetKey(KeyCode.S))
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
            _player.GetComponent<PersonController>().enabled = true;
            _state = 0;
        }
        _animator.SetInteger("State", _state);
    }
}
