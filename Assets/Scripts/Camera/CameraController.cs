using Photon.Pun;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitiveMouse = 100f;
    [SerializeField] private Transform Player;
    [SerializeField] private PhotonView _photonView;
    private float xRotation;
    private float yRotation;
    private const int Y_LOOK_LIMIT = 55;
    private const int COEFFICIENT = 3; 

    void Start()
    {
        sensitiveMouse = PlayerPrefs.GetInt("Sensetivity") * COEFFICIENT;
        Cursor.lockState = CursorLockMode.Locked;
        _photonView = Player.GetComponent<PhotonView>();
        if (!_photonView.IsMine)
        {
            Destroy(GetComponent<Camera>());
            Destroy(GetComponent<AudioListener>());
        }
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
        float mouseX = Input.GetAxis("Mouse X") * sensitiveMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitiveMouse * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -Y_LOOK_LIMIT, Y_LOOK_LIMIT);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Player.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
