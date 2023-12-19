using UnityEngine;

public class OpenStore : MonoBehaviour
{
    [SerializeField] private GameObject store;
    [SerializeField] private GameObject buyMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<PersonController>().enabled = !GetComponent<PersonController>().enabled;
            Cursor.visible = !Cursor.visible;
            if (!Cursor.visible)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
            store.SetActive(!store.activeSelf);
            buyMenu.SetActive(false);
        }
    }
}
