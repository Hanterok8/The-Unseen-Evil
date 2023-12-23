using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PersonController : MonoBehaviour
{
    [SerializeField] public float _speed = 3f;
    [SerializeField] public float _maxSpeed = 4f;
    [SerializeField] public float _minSpeed = 2f;
    [SerializeField] private bool _ground;
    private PhotonView _photonView;
    private Animator _animator;
    public int _state;
    private CrouchControlller _crouchController;
    private StaminaSettings _staminaSettings;
    public bool _isRunning;
    public Vector2 AxesSpeed;

    private void Start()
    {
        _staminaSettings = GetComponent<StaminaSettings>();
        _crouchController = GetComponent<CrouchControlller>();
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponent<Animator>();
        _photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (!_photonView.IsMine) return;
        AxesSpeed = new Vector2(Input.GetAxis("Horizontal") * _speed, Input.GetAxis("Vertical") * _speed);
        _isRunning = Input.GetKey(KeyCode.LeftShift);
        MovementInput();

    }
    private void MovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            
            _crouchController.enabled = false;
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            _state = 1;
            if (_isRunning && _staminaSettings._playerStamina > 0)
            {
                _crouchController.enabled = false;
                transform.Translate(Vector3.forward * _maxSpeed * Time.deltaTime);
                _state = 2;
            }
        }
        else
        {
            _crouchController.enabled = true;
            _state = 0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _crouchController.enabled = false;
            transform.Translate(Vector3.left * _minSpeed * Time.deltaTime);
            _state = 6;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _crouchController.enabled = false;
            transform.Translate(Vector3.right * _minSpeed * Time.deltaTime);
            _state = 5;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _crouchController.enabled = false;
            transform.Translate(Vector3.back * _minSpeed * Time.deltaTime);
            _state = 3;
        }
        _animator.SetInteger("State", _state);
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
