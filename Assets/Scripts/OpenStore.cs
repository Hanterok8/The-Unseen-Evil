using UnityEngine;

public class OpenStore : MonoBehaviour
{
    [SerializeField] public Weapon weapon;
    [SerializeField] public CameraController cameraController;
    public bool isStoreOpened;
    private StoreElements storeElements;
    private bool isInShopCircleCollider;
    private IsAimodipsis aimodipsis;
    private bool isOpenedStore = false;
    private void Start()
    {
        storeElements = FindObjectOfType<StoreElements>();
        aimodipsis = GetPlayer().GetComponent<IsAimodipsis>();
        isStoreOpened = false;
    }
    private GameObject GetPlayer()
    {
        CurrentPlayer[] players = FindObjectsOfType<CurrentPlayer>();
        foreach (CurrentPlayer player in players)
        {
            if (player.CurrentPlayerModel == gameObject)
            {
                return player.gameObject;
            }
        }
        return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInShopCircleCollider && !aimodipsis.isAimodipsis)
        {
            isOpenedStore = !isOpenedStore;
            SwapPlayerMovementState(isOpenedStore);
            OpenUIStore();
        }
    }
    public virtual void SwapPlayerMovementState(bool enableState)
    {
        cameraController.enabled = enableState;
        weapon.enabled = enableState;
        if (!enableState)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = !enableState;
    }

    private void OpenUIStore()
    {
        
        GameObject Blur = storeElements.BlurBackground;
        Blur.SetActive(!Blur.activeSelf);
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
                SwapPlayerMovementState(false);
                OpenUIStore();
            }

        }
         
    }
}
