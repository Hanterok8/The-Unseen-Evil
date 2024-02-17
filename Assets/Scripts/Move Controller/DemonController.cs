using Photon.Pun;
using System;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    [SerializeField] public Animator _playerAnimator;
    [SerializeField] private Rigidbody _playerRigidbody;

    [SerializeField] private Transform _mainCamera;
    [SerializeField] private float _movementSpeed = 2f;
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
            AxesSpeed = new Vector2(Input.GetAxis("Horizontal") * _movementSpeed, Input.GetAxis("Vertical") * _movementSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (_photonView.IsMine)
            _playerRigidbody.velocity = new Vector3
                (_movementVector.x * _movementSpeed, _playerRigidbody.velocity.y, _movementVector.z * _movementSpeed);
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
            _movementSpeed = 2f;
            _photonView.RPC(nameof(Run), RpcTarget.All, false);
        }

        return movementVector;
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
    private void Run(bool isRunning)
    {
        _playerAnimator.SetBool("isRunning", isRunning);
    }
    [PunRPC]

    private void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            newFOV = 75;
            _movementSpeed = 4f;
            _photonView.RPC(nameof(Run), RpcTarget.All, true);
        }
    }
}
