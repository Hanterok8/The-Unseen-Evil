using Photon.Pun;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivityMouse = 100f;
    [SerializeField] private Transform Player;
    private PhotonView _photonView;
    private CharacterController characterController;
    private DemonController demonController;
    private float xRotation;
    private float yRotation;
    private const int Y_LOOK_LIMIT = 55;
    private const int COEFFICIENT = 3;
    private Camera camera;
    private int currentFOV = 60;
    public Transform aimPose;
    public float AimSmoothspeed = 20f;
    public LayerMask aimMask;
    private void Awake()
    {
        characterController = Player.GetComponent<CharacterController>();
    }
    void Start()
    {
        camera = GetComponent<Camera>();
        sensitivityMouse = PlayerPrefs.GetInt("Sensitivity") * COEFFICIENT;
        Cursor.lockState = CursorLockMode.Locked;
        _photonView = Player.GetComponent<PhotonView>();
        if (!_photonView.IsMine)
        {
            Destroy(GetComponent<Camera>());
            Destroy(GetComponent<AudioListener>());
        }
        characterController.onChangedFOV += SetNewFOV;
    }
    private void OnDisable()
    {
        characterController.onChangedFOV -= SetNewFOV;
    }
    void FixedUpdate()
    {
        if (_photonView == null)
        {
            _photonView = Player.GetComponent<PhotonView>();
            if (!_photonView.IsMine)
            {
                Destroy(GetComponent<Camera>());
                Destroy(GetComponent<AudioListener>());
            }
        }
        if (!_photonView.IsMine) return;
        float mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -Y_LOOK_LIMIT, Y_LOOK_LIMIT);

        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPose.position = Vector3.Lerp(aimPose.position,hit.point,AimSmoothspeed*Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Player.rotation = Quaternion.Euler(0, yRotation + 1, 0);
        
    }
    private void SetNewFOV(int newCameraFOV)
    {
        if (currentFOV == newCameraFOV) return;
        StopAllCoroutines();
        StartCoroutine(InterpolateToNewFOV(newCameraFOV, currentFOV));
        currentFOV = newCameraFOV;
    }

    private IEnumerator InterpolateToNewFOV(int newFOV, int previousFOV)
    {
        int step = previousFOV > newFOV ? -1 : 1;
        while (camera.fieldOfView != newFOV)
        {
            camera.fieldOfView += step;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public IEnumerator ShakeCamera()
    {
        while (true)
        {
            camera.transform.localPosition = new Vector3
                    (Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * 0.3f;
        }
    }
}
