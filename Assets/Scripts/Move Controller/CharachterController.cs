using UnityEngine;

public class CharachterController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Rigidbody _playerRigidbody;

    [SerializeField] private Transform _mainCamera;
    [SerializeField] private float _movementSpeed = 5f;

    private Vector3 _movementVector;
    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody>();

        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    void Update()
    {
        _movementVector = Movement();
    }

    private void FixedUpdate()
    {
        _playerRigidbody.velocity = new Vector3(_movementVector.x * _movementSpeed, _playerRigidbody.velocity.y,_movementVector.z * _movementSpeed);
    }

    private Vector3 Movement()
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
        _playerAnimator.SetFloat("Horizontal", relativeVector.x);
        _playerAnimator.SetFloat("Vertical", relativeVector.z);

        return movementVector;
    }
}
