using Photon.Pun;
using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] public Animator _playerAnimator;
    [SerializeField] private Rigidbody _playerRigidbody;

    [SerializeField] private Transform _mainCamera;
    [SerializeField] private float _movementWalkSpeed = 4f;
    private GameObject _menu;
    public float currentSpeed = 0f;
    private PhotonView _photonView;
    public bool isRunning;
    private Vector3 _movementVector;
    public Vector2 AxesSpeed;
    public Action<int> onChangedFOV;
    private int newFOV = 60;
    public bool isFrozen;
    private StaminaSettings stamina;

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

        stamina.onStaminaUpdated += CheckNewStamina;
    }

    private void OnDisable()
    {
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
        
        onChangedFOV?.Invoke(newFOV);
        AxesSpeed = new Vector2(Input.GetAxis("Horizontal") * _movementWalkSpeed, Input.GetAxis("Vertical") * _movementWalkSpeed);
        _playerRigidbody.velocity = new Vector3
            (_movementVector.x * _movementWalkSpeed, _playerRigidbody.velocity.y, _movementVector.z * _movementWalkSpeed);

    }

    private void CheckNewStamina()
    {
        if (stamina && stamina._playerStamina <= 0)
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && stamina._playerStamina > 0)
        {
            isRunning = true;
            _photonView.RPC(nameof(ChangeRunAnimation), RpcTarget.All, 1f);
            newFOV = 75;
            _movementWalkSpeed = 7f;
        }
    }
}
