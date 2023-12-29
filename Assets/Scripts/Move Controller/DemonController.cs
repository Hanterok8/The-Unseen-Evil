using Photon.Pun;
using System.Collections;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    [SerializeField] public float _speed = 3f;
    [SerializeField] public float _maxSpeed = 4f;
    [SerializeField] public float _minSpeed = 2f;
    [SerializeField] private bool _ground;
    private Animator _animator;
    public int _state;
    [SerializeField] private Rigidbody _Rb;
    private float _force = 10f;
    private bool isInSuperJumpCoolDown = false;
    [SerializeField] private PhotonView _photonView;
    public bool _isRunning;
    public Vector2 AxesSpeed;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_photonView == null) _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine) return;
        AxesSpeed = new Vector2(Input.GetAxis("Horizontal") * _speed, Input.GetAxis("Vertical") * _speed);
        _isRunning = Input.GetKey(KeyCode.LeftShift);
        MovementInput();
    }
    private void MovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            _state = 1;
            if (_isRunning)
            {
                transform.Translate(Vector3.forward * _maxSpeed * Time.deltaTime);
                _state = 2;
            }
        }
        else
        {
            _state = 0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _minSpeed * Time.deltaTime);
            _state = 6;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _minSpeed * Time.deltaTime);
            _state = 5;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * _minSpeed * Time.deltaTime);
            _state = 3;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isInSuperJumpCoolDown)
        {
            StartCoroutine(DoSuperJump());
        }
        _animator.SetInteger("State", _state);
    }
    IEnumerator DoSuperJump()
    {
        _Rb.AddForce(Vector3.up * _force / 2, ForceMode.Impulse);
        _Rb.AddForce(transform.forward * _force, ForceMode.Impulse);
        isInSuperJumpCoolDown = true;
        yield return new WaitForSeconds(3);
        isInSuperJumpCoolDown = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _maxSpeed = 0;
            _speed = 0;
        }
        else
        {
            _maxSpeed = 4;
            _speed = 3;
        }
    }
}
