using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuOpener : MonoBehaviour
{
    private GameObject gameMenu;
    private OpenStore openStore;

    void Start()
    {
        GameObject playerModel = GetComponent<CurrentPlayer>().CurrentPlayerModel;
        openStore = playerModel.GetComponent<OpenStore>();
        gameMenu = FindObjectOfType<GameMenuElements>().gameMenu;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !openStore.isStoreOpened)
        {
            SwapMenuUIState();
        }
    }
    private void SwapMenuUIState()
    {
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.enabled = !cameraController.enabled;
        gameMenu.SetActive(!gameMenu.activeSelf);
        if (Cursor.visible)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = !Cursor.visible;
    }
}
