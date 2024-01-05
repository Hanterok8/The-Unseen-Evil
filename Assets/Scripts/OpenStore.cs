using UnityEngine;

public class OpenStore : MonoBehaviour
{
    [SerializeField] private GunSystem gunSystem;
    private StoreElements storeElements;
    private bool isInShopCircleCollider;
    private bool isStoreOpened;
    private void Start()
    {
        storeElements = FindObjectOfType<StoreElements>();
        isStoreOpened = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInShopCircleCollider && !IsAimodipsis.isAimodipsis)
        {
            SwapStoreUIState();
        }
    }
    private void SwapStoreUIState()
    {
        gunSystem.enabled = !gunSystem.enabled;
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
