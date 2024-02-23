using UnityEngine;

public class CrystalQuestUIOpener : MonoBehaviour
{
    private CrystalElements crystalElements;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        crystalElements = FindObjectOfType<CrystalElements>();
    }
    private void SwapPlayerMovementState()
    {
        characterController.enabled = false;
        GameObject crystalCanvas = crystalElements.crystalCanvas;
        crystalCanvas.SetActive(true);
        characterController._playerAnimator.enabled = false;
        characterController.AnimatorStateChange(new Vector3(0, 0, 0));
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public new void Open()
    {
        SwapPlayerMovementState();
    }
}
