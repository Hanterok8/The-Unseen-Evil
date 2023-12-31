using UnityEngine;

public class OpenStore : MonoBehaviour
{
    private StoreElements storeElements;
    private PersonController personController;
    private bool isInShopCircleCollider;
    private void Start()
    {
        personController = GetComponent<PersonController>();
        storeElements = Object.FindFirstObjectByType<StoreElements>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInShopCircleCollider && !IsAimodipsis.isAimodipsis)
        {
            personController.enabled = !personController.enabled;
            Cursor.visible = !Cursor.visible;
            if (!Cursor.visible)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
            storeElements.store.SetActive(!storeElements.store.activeSelf);
            storeElements.buyMenu.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("shop_circle"))
            isInShopCircleCollider = true;
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("shop_circle"))
            isInShopCircleCollider = false;
    }
}
