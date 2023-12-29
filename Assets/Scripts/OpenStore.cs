using UnityEngine;

public class OpenStore : MonoBehaviour
{
    [SerializeField] private GameObject store;
    [SerializeField] private GameObject buyMenu;
    private PersonController personController;
    private bool isInShopCircleCollider;
    private IsAimodipsis aimodipsis;
    private void Start()
    {
        aimodipsis = Object.FindFirstObjectByType<IsAimodipsis>();
        personController = GetComponent<PersonController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInShopCircleCollider && !aimodipsis.isAimodipsis)
        {
            personController.enabled = !personController.enabled;
            Cursor.visible = !Cursor.visible;
            if (!Cursor.visible)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
            store.SetActive(!store.activeSelf);
            buyMenu.SetActive(false);
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
