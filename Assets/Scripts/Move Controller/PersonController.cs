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
    public bool isInhabitantFrozen;
    public Animator animator;
    private PhotonView _photonView;
    private CrouchControlller _crouchController;
    private RifleController _rifleController;
    private HoldController _holdController;
    private StaminaSettings _staminaSettings;

    private void Start()
    {
        _rifleController = GetComponent<RifleController>();
        _staminaSettings = GetComponent<StaminaSettings>();
        _crouchController = GetComponent<CrouchControlller>();
        _holdController = GetComponent<HoldController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
        _photonView = GetComponent<PhotonView>();
        isInhabitantFrozen = false;
    }
    private void Update()
    {
        if (_photonView == null) _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine) return;
        AxesSpeed = new Vector2(Input.GetAxis("Horizontal") * _speed, Input.GetAxis("Vertical") * _speed);
        isRunning = Input.GetKey(KeyCode.LeftShift);
        if (!isInhabitantFrozen)
        {
            MovementInput();
        }

    }
    private Animator GetAnimator()
    {
        if (animator == null) animator = GetComponent<Animator>();
        return animator;
    }
    private void MovementInput()
    {
        Forward();
        Backward();
        Right();
        Left();
        ChangePlayerAnimation(state);
    }
    public void SetNewFrozenValue(bool isFrozenNow)
    => isInhabitantFrozen = isFrozenNow;
    public void SetKinematicModeForRigidbody()
    => GetComponent<Rigidbody>().isKinematic = true;
    public void ChangePlayerAnimation(int playerState)
    {
        _photonView.RPC(nameof(ChangeAnimationRPC), RpcTarget.All, playerState);
    }
    [PunRPC]
    private void ChangeAnimationRPC(int animationState)
    {
        animator.SetInteger("State", animationState);
    }
    public void Forward()
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
    }
    public void Backward()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _crouchController.enabled = false;
            transform.Translate(Vector3.back * _minSpeed * Time.deltaTime);
            state = 3;
        }
    }
    public void Left()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _crouchController.enabled = false;
            transform.Translate(Vector3.left * _minSpeed * Time.deltaTime);
            _maxSpeed = 3;
            state = 6;
        }
        else
        {
            _maxSpeed = 4;
        }
    }
    public void Right()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _crouchController.enabled = false;
            transform.Translate(Vector3.right * _minSpeed * Time.deltaTime);
            _maxSpeed = 3;
            state = 5;
        }
        else
        {
            _maxSpeed = 4;
        }
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
