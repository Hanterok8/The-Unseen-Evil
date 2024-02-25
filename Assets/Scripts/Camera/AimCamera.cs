using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCamera : MonoBehaviour
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
    private void Awake()
    {
        characterController = Player.GetComponent<CharacterController>();
        demonController = Player.GetComponent<DemonController>();
    }
    void Start()
    {
        camera = GetComponent<Camera>();
        sensitivityMouse = PlayerPrefs.GetInt("Sensitivity") * COEFFICIENT;
        Cursor.lockState = CursorLockMode.Locked;
        //_photonView = Player.GetComponent<PhotonView>();
        _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine)
        {
            Destroy(GetComponent<Camera>());
            Destroy(GetComponent<AudioListener>());
        }
    }
    private void OnEnable()
    {
        if (characterController)
            characterController.onChangedFOV += SetNewFOV;
        else
            demonController.onChangedFOV += SetNewFOV;
    }
    private void OnDisable()
    {
        if (characterController)
            characterController.onChangedFOV -= SetNewFOV;
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
        float mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;

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
    public IEnumerator ShakeCamera()
    {
        while (true)
        {
            camera.transform.localPosition = new Vector3
                    (Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * 0.3f;
            yield return null;
        }
    }
}
