using Photon.Pun;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitiveMouse = 100f;

    private float mouseX;
    private float mouseY;

    private float yRotation;
    private float xRotation;

    private Transform Player;
    private PhotonView _photonView;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        _photonView = Player.GetComponent<PhotonView>();
    }
    void Update()
    {
        if (!_photonView.IsMine) return;
        mouseX = Input.GetAxis("Mouse X") * sensitiveMouse * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitiveMouse * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -55f, 55f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Player.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
