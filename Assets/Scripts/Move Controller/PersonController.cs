using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PersonController : MonoBehaviour
{
    [SerializeField] public float _speed = 3f;
    [SerializeField] public float _maxSpeed = 4f;
    [SerializeField] public float _minSpeed = 2f;
    [SerializeField] private bool _ground;
    public Vector2 AxesSpeed;
    public int state;
    public bool isRunning;
    public bool isFrozen;
    public Animator animator;
    private PhotonView _photonView;
    private CrouchControlller _crouchController;
    private StaminaSettings _staminaSettings;

    private void Start()
    {
        _staminaSettings = GetComponent<StaminaSettings>();
        _crouchController = GetComponent<CrouchControlller>();
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        _photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (_photonView == null) _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine) return;
        AxesSpeed = new Vector2(Input.GetAxis("Horizontal") * _speed, Input.GetAxis("Vertical") * _speed);
        isRunning = Input.GetKey(KeyCode.LeftShift);
        if(!isFrozen) MovementInput();

    }
    private Animator GetAnimator()
    {
        if(animator == null) animator = GetComponent<Animator>();
        return animator;
    }
    private void MovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            
            _crouchController.enabled = false;
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            state = 1;
            if (isRunning && _staminaSettings._playerStamina > 0)
            {
                _crouchController.enabled = false;
                transform.Translate(Vector3.forward * _maxSpeed * Time.deltaTime);
                state = 2;
            }
        }
        else
        {
            _crouchController.enabled = true;
            state = 0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _crouchController.enabled = false;
            transform.Translate(Vector3.left * _minSpeed * Time.deltaTime);
            state = 6;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _crouchController.enabled = false;
            transform.Translate(Vector3.right * _minSpeed * Time.deltaTime);
            state = 5;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _crouchController.enabled = false;
            transform.Translate(Vector3.back * _minSpeed * Time.deltaTime);
            state = 3;
        }
        
        _photonView.RPC(nameof(ChangeAnimation), RpcTarget.All, state);
    }
    [PunRPC]
    private void ChangeAnimation(int animationState)
    {
        animator.SetInteger("State", animationState);
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
