using Photon.Pun;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitiveMouse = 100f;
    private float yRotation;
    private float xRotation;
    private const int yLookLimit = 55;

    [SerializeField] private Transform Player;
    private PhotonView _photonView;
    void Start()
    {
        Player = Player.transform.parent;
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
        if (!_photonView.IsMine) return;
        float mouseX = Input.GetAxis("Mouse X") * sensitiveMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitiveMouse * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -yLookLimit, yLookLimit);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Player.transform.GetChild(0).rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
