using Photon.Pun;
using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] public Animator _playerAnimator;
    [SerializeField] private Rigidbody _playerRigidbody;

    [SerializeField] private Transform _mainCamera;
    [SerializeField] private float _movementWalkSpeed = 2f;
    private PhotonView _photonView;
    public bool isRunning;
    private Vector3 _movementVector;
    public Vector2 AxesSpeed;
    public Action<int> onChangedFOV;
    private int newFOV = 60;
    void Start()
    {
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
    }
    void Update()
    {
        if (_photonView.IsMine)
        {
            _movementVector = GetMovement();
            onChangedFOV?.Invoke(newFOV);
            AxesSpeed = new Vector2(Input.GetAxis("Horizontal") * _movementWalkSpeed, Input.GetAxis("Vertical") * _movementWalkSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (_photonView.IsMine)
            _playerRigidbody.velocity = new Vector3
                (_movementVector.x * _movementWalkSpeed, _playerRigidbody.velocity.y, _movementVector.z * _movementWalkSpeed);
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
            newFOV = 60;
            isRunning = false;
            _movementWalkSpeed = 2f;
            Run(false);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Crouch(false);
        }
        
        return movementVector;
    }
    [PunRPC]
    private void AnimatorStateChange(Vector3 relativeVector)
    {
        _playerAnimator.SetFloat("Horizontal", relativeVector.x);
        _playerAnimator.SetFloat("Vertical", relativeVector.z);
    }

    [PunRPC]
    private void Run(bool isRunning)
    {
        _playerAnimator.SetBool("isRunning", isRunning);
    }
    [PunRPC]
    private void Crouch(bool isCrouching)
    {
        _playerAnimator.SetBool("isCrouch", isCrouching);
    }
    private void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            newFOV = 75;
            _movementWalkSpeed = 4f;
            Run(true);
        }
    }
}
