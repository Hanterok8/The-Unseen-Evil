using Photon.Pun;
using System.Collections;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    [SerializeField] public float _speed = 3f;
    [SerializeField] public float _maxSpeed = 4f;
    [SerializeField] public float _minSpeed = 2f;
    [SerializeField] private bool _ground;
    [SerializeField] private PhotonView _photonView;
    public bool isDemonFrozen;
    public int _state;
    public bool _isRunning;
    public Vector2 AxesSpeed;
    private Animator animator;
    private Rigidbody _rb;
    private float _force = 10f;
    private bool isInSuperJumpCoolDown = false;

    private void Start()
    {
        isDemonFrozen = false;
        _rb = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_photonView == null) _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine) return;
        AxesSpeed = new Vector2(Input.GetAxis("Horizontal") * _speed, Input.GetAxis("Vertical") * _speed);
        _isRunning = Input.GetKey(KeyCode.LeftShift);
        if(!isDemonFrozen) MovementInput();
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
        ChangePlayerAnimation(_state);
    }
    IEnumerator DoSuperJump()
    {
        _rb.AddForce(Vector3.up * _force / 2, ForceMode.Impulse);
        _rb.AddForce(transform.forward * _force, ForceMode.Impulse);
        isInSuperJumpCoolDown = true;
        yield return new WaitForSeconds(3);
        isInSuperJumpCoolDown = false;
    }
    private void ChangePlayerAnimation(int playerState)
    {
        _photonView.RPC(nameof(ChangeAnimationRPC), RpcTarget.All, playerState);
    }
    private Animator GetAnimator()
    {
        if (animator == null) animator = GetComponent<Animator>();
        return animator;
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
    [PunRPC]
    private void ChangeAnimationRPC(int animationState)
    {
        GetAnimator().SetInteger("State", animationState);
    }
}
