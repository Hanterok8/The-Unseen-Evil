using Photon.Pun;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensetivityMouse = 100f;
    [SerializeField] private Transform Player;
    [SerializeField] private PhotonView _photonView;
    private PersonController personController;
    private DemonController demonController;
    private float xRotation;
    private float yRotation;
    private const int Y_LOOK_LIMIT = 55;
    private const int COEFFICIENT = 3;
    private Camera camera;
    private int currentFOV = 60;
    private void Awake()
    {
        personController = Player.GetComponent<PersonController>();
        demonController = Player.GetComponent<DemonController>();
    }
    void Start()
    {
        camera = GetComponent<Camera>();
        sensetivityMouse = PlayerPrefs.GetInt("Sensetivity") * COEFFICIENT;
        Cursor.lockState = CursorLockMode.Locked;
        _photonView = Player.GetComponent<PhotonView>();
        if (!_photonView.IsMine)
        {
            Destroy(GetComponent<Camera>());
            Destroy(GetComponent<AudioListener>());
        }
    }
    private void OnEnable()
    {
        if (personController)
            personController.onChangedFOV += SetNewFOV;
        else
            demonController.onChangedFOV += SetNewFOV;
    }
    private void OnDisable()
    {
        if (personController)
            personController.onChangedFOV -= SetNewFOV;
        else
            demonController.onChangedFOV -= SetNewFOV;
    }
    void Update()
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
        float mouseX = Input.GetAxis("Mouse X") * sensetivityMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensetivityMouse * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -Y_LOOK_LIMIT, Y_LOOK_LIMIT);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Player.rotation = Quaternion.Euler(0, yRotation+1, 0);
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
}
