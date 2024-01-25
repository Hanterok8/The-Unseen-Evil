using UnityEngine;

public class OpenStore : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private Camera playerCamera;
    private CameraController cameraController;
    private StoreElements storeElements;
    private bool isInShopCircleCollider;
    private bool isStoreOpened;

    private IsAimodipsis aimodipsis;
    private void Start()
    {
        cameraController = playerCamera.GetComponent<CameraController>();
        storeElements = FindObjectOfType<StoreElements>();
        aimodipsis = GetComponent<IsAimodipsis>();
        isStoreOpened = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInShopCircleCollider && !aimodipsis.isAimodipsis)
        {
            SwapStoreUIState();
        }
    }
    private void SwapStoreUIState()
    {
        cameraController.enabled = !cameraController.enabled;
        weapon.enabled = !weapon.enabled;
        if (Cursor.visible)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = !Cursor.visible;
        storeElements.store.SetActive(!storeElements.store.activeSelf);
        storeElements.buyMenu.SetActive(false);
        isStoreOpened = !isStoreOpened;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("shop_circle"))
        {
            isInShopCircleCollider = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("shop_circle"))
        {
            isInShopCircleCollider = false;

            if (isStoreOpened)
            {
                SwapStoreUIState();
            }

        }
         
    }
}
