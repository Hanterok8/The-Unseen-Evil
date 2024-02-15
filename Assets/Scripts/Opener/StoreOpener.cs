using UnityEngine;

public class StoreOpener : MonoBehaviour, IOpenable
{
    private StoreElements storeElements;
    private void Start()
    {
        storeElements = FindObjectOfType<StoreElements>();
    }

    public virtual void SwapPlayerMovementState(bool enableState)
    {
        if (!enableState)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = !enableState;
        GameObject Blur = storeElements.BlurBackground;
        Blur.SetActive(!Blur.activeSelf);
        storeElements.store.SetActive(!storeElements.store.activeSelf);
        storeElements.buyMenu.SetActive(false);
    }

    public new void Open(bool stateChangeTO)
    {
        SwapPlayerMovementState(stateChangeTO);
    }
}
