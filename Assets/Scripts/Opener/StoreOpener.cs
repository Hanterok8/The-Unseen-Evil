using UnityEngine;

public class StoreOpener : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Weapon weapon;
    private StoreElements storeElements;
    private void Start()
    {
        storeElements = FindObjectOfType<StoreElements>();
    }

    public void SwapPlayerMovementState()
    {
        cameraController.enabled = !cameraController.enabled;
        weapon.enabled = !weapon.enabled;
        if (Cursor.visible)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = !Cursor.visible;
        GameObject Blur = storeElements.BlurBackground;
        Blur.SetActive(!Blur.activeSelf);
        storeElements.store.SetActive(!storeElements.store.activeSelf);
        storeElements.buyMenu.SetActive(false);
    }

    public new void Open()
    {
        SwapPlayerMovementState();
    }
}
