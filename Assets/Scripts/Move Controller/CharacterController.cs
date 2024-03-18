using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] public Animator _playerAnimator;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private float _movementWalkSpeed = 4f;
    [SerializeField] private GameObject _menu;
    public bool isRunning;
    public Vector2 AxesSpeed;
    public Action<int> onChangedFOV;
    public bool isFrozen;
    private PhotonView _photonView;
    private Vector3 _movementVector;
    private int newFOV = 60;
    private StaminaSettings stamina;
    private int currentStamina;

    void Start()
    {
        _menu = GameObject.FindGameObjectWithTag("menu");
        stamina = GetComponent<StaminaSettings>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _photonView = GetComponent<PhotonView>();
        _playerAnimator = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody>();

        transform.rotation = Quaternion.Euler(Vector3.zero);
        if (!_photonView.IsMine)
        {
            Destroy(_mainCamera.GetComponent<Camera>());
            Destroy(_mainCamera.GetComponent<AudioListener>());
        }
        if (stamina == null) return;
        stamina.onStaminaUpdated += CheckNewStamina;
    }

    private void OnDisable()
    {
        if (stamina == null) return;
        stamina.onStaminaUpdated -= CheckNewStamina;
    }

    void Update()
    {
        if (_photonView.IsMine)
        {
            if (!isFrozen) _movementVector = GetMovement();
        }
    }

    private void FixedUpdate()
    {
        if (!_photonView.IsMine) return;
        currentStamina = gameObject.layer == 8 ? 100 : stamina._playerStamina;   
        onChangedFOV?.Invoke(newFOV);
        AxesSpeed = new Vector2(Input.GetAxis("Horizontal") * _movementWalkSpeed, Input.GetAxis("Vertical") * _movementWalkSpeed);
        _playerRigidbody.velocity = new Vector3
            (_movementVector.x * _movementWalkSpeed, _playerRigidbody.velocity.y, _movementVector.z * _movementWalkSpeed);

    }

    private void CheckNewStamina()
    {
        Debug.Log(currentStamina.ToString() + " <- Stamina, if = " + (currentStamina <= 0));
        if (currentStamina <= 1)
        {
            SetRunningStateFalse();
        }
    }
    private Vector3 GetMovement()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraR = _mainCamera.right;
        Vector3 cameraF = _mainCamera.forward;

        cameraR.y = 0;
        cameraF.y = 0;

        Vector3 movementVector = cameraF.normalized * vertical + cameraR.normalized * horizontal;
        movementVector = Vector3.ClampMagnitude(movementVector, 1);


        Vector3 relativeVector = transform.InverseTransformDirection(movementVector);
        AnimatorStateChange(relativeVector);
        ControllerDemonAttack();
        OnRun();
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SetRunningStateFalse();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _menu.SetActive(!_menu.activeSelf);
            if (Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
            }

            Cursor.visible = !Cursor.visible;
        }
        return movementVector;
    }

    private void SetRunningStateFalse()
    {
        isRunning = false;
        _photonView.RPC(nameof(ChangeRunAnimation), RpcTarget.All, 0f);
        newFOV = 60;
        _movementWalkSpeed = 4f;
    }

    public void AnimatorStateChange(Vector3 relativeVector3)
    {
        _photonView.RPC(nameof(AnimatorStateChangeRPC), RpcTarget.All, relativeVector3);
    }
    [PunRPC]
    private void AnimatorStateChangeRPC(Vector3 relativeVector)
    {
        _playerAnimator.SetFloat("Horizontal", relativeVector.x);
        _playerAnimator.SetFloat("Vertical", relativeVector.z);
    }
    [PunRPC]
    private void ChangeRunAnimation(float value)
    {
        _playerAnimator.SetFloat("Speed", value);
    }
    private void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && currentStamina > 0)
        {
            isRunning = true;
            _photonView.RPC(nameof(ChangeRunAnimation), RpcTarget.All, 1f);
            newFOV = 75;
            _movementWalkSpeed = 7f;
        }
    }

    public void SetAimingAnimation(bool newState)
    {
        _photonView.RPC(nameof(SetAimingAnimationRPC), RpcTarget.All, newState);
    }
    [PunRPC]
    private void SetAimingAnimationRPC(bool newState)
    {
        _playerAnimator.SetBool("isAiming", newState);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            _movementWalkSpeed = 0;
            _playerAnimator.SetBool("isWall" , true);
        }
        else
        {
            _movementWalkSpeed = 4;
            _playerAnimator.SetBool("isWall", false);
        }
    }
    private void ControllerDemonAttack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _playerAnimator.SetBool("IsAttack", true);
            StartCoroutine(HandAttack());
        }
        if (Input.GetMouseButtonDown(1))
        {
            _playerAnimator.SetBool("isJumpAttack", true);
            StartCoroutine(Culdayn());
        }
    }
    private IEnumerator Culdayn()
    {
        yield return new WaitForSeconds(2);
        _playerAnimator.SetBool("isJumpAttack", false);
    }
    private IEnumerator HandAttack()
    {
        yield return new WaitForSeconds(1);
        _playerAnimator.SetBool("IsAttack", false);
    }
}
