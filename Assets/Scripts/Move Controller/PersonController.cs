using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class PersonController : MonoBehaviourPunCallbacks
{
    [SerializeField] public float _speed = 3f;
    [SerializeField] public float _maxSpeed = 4f;
    [SerializeField] public float _minSpeed = 2f;
    [SerializeField] private bool _ground;
    [SerializeField] private GameObject spectatorPlayer;
    public Vector2 AxesSpeed;
    public int state;
    public bool isRunning;
    public bool isInhabitantFrozen;
    public Animator animator;
    public Action<int> onChangedFOV;
    private PhotonView _photonView;
    private CrouchControlller _crouchController;
    private RifleController _rifleController;
    private HoldController _holdController;
    private StaminaSettings _staminaSettings;
    private int newFOV;
    public Action onTransformedToSpectator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        _rifleController = GetComponent<RifleController>();
        _staminaSettings = GetComponent<StaminaSettings>();
        _crouchController = GetComponent<CrouchControlller>();
        _holdController = GetComponent<HoldController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _photonView = GetComponent<PhotonView>();
        isInhabitantFrozen = false;
    }
    private void Update()
    {
        if (_photonView == null) _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine) return;
        AxesSpeed = new Vector2(Input.GetAxis("Horizontal") * _speed, Input.GetAxis("Vertical") * _speed);
        isRunning = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.L))
        {
            KickPlayer();
        }
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
        if (Input.GetKey(KeyCode.K))
        {
            state = 7;
            Debug.Log("state: " + state);
        }
        if (Input.GetKey(KeyCode.W))
        {
            newFOV = 60;
            _crouchController.enabled = false;
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            state = 1;
            if (isRunning && _staminaSettings._playerStamina > 0)
            {
                newFOV = 75;
                _crouchController.enabled = false;
                transform.Translate(Vector3.forward * _maxSpeed * Time.deltaTime);
                state = 2;
            }
        }
        else
        {
            newFOV = 60;
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
        onChangedFOV?.Invoke(newFOV);
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

    internal void LookAtDemon(Transform demon)
    {
        //transform.LookAt(demon);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(demon.position), 50 * Time.deltaTime);
    }
    [PunRPC]
    internal void KickPlayer()
    {
        if (!_photonView.IsMine) return;
        CurrentPlayer[] players = FindObjectsOfType<CurrentPlayer>();
        GameObject spectator = Instantiate(spectatorPlayer);
        foreach (CurrentPlayer player in players)
        {
            if (player.CurrentPlayerModel == gameObject)
            {
                //Destroy(player.gameObject);
                player.GetComponent<CurrentPlayer>().CurrentPlayerModel = spectator;
                break;
            }
        }
        Destroy(gameObject); 
        onTransformedToSpectator?.Invoke();
        //PhotonNetwork.Disconnect();

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Menu");
    }

}
