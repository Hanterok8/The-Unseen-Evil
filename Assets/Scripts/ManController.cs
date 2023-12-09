using UnityEngine;

public class ManController : MonoBehaviour
{
    [SerializeField] public GameObject _player;
    [SerializeField] public float _speed = 3f;
    [SerializeField] public float _maxSpeed = 4f;
    [SerializeField] public float _minSpeed = 2f;
    [SerializeField] private bool _ground;
    [SerializeField] private float _jumpPower = 4f;
    [SerializeField] public Animator _animator;
    public int _state;

    public Rigidbody Rb;
    void Update()
    {
        GetInput();
    }
    public void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _player.GetComponent<CrouchControlller>().enabled = false;
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            _state = 1;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * _maxSpeed * Time.deltaTime);
                _state = 2;
            }
        }
        else
        {
            _player.GetComponent<CrouchControlller>().enabled = true;
            _state = 0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _player.GetComponent<CrouchControlller>().enabled = false;
            transform.Translate(Vector3.left * _minSpeed * Time.deltaTime);
            _state = 6;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _player.GetComponent<CrouchControlller>().enabled = false;
            transform.Translate(Vector3.right * _minSpeed * Time.deltaTime);
            _state = 5;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _player.GetComponent<CrouchControlller>().enabled = false;
            transform.Translate(Vector3.back * _minSpeed * Time.deltaTime);
            _state = 3;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumb();
        }
        _animator.SetInteger("State", _state);
    }
    void Jumb()
    {
        _animator.SetTrigger("Jump");
        Rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
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
