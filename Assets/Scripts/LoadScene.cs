using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject exitMenuUI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwapMenuUIState();
        }
    }
    private void SwapMenuUIState()
    {
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.enabled = !cameraController.enabled;
        exitMenuUI.SetActive(!exitMenuUI.activeSelf);
        if (Cursor.visible)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = !Cursor.visible;
    }
    public void Load(string level)
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(level);
    }
}
