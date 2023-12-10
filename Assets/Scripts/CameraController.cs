using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitiveMouse = 100f;

    private float mouseX;
    private float mouseY;

    private float yRotation;
    private float xRotation;

    [SerializeField] private Transform Player;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitiveMouse * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitiveMouse * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Player.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
