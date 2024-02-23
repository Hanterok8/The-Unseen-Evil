using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AltarConnectingItems : MonoBehaviour
{
    [SerializeField] private GameObject gameEndingImage;
    private bool isConnected = false;
    private int activatedPlatforms = 0;
    private const int REQUIRED_ITEMS = 2;
    private void Update()
    {
        if (isConnected)
            Invoke(nameof(OnItemsConnected), 1f);
    }
    public void AddActivatedPlatform()
    {
        activatedPlatforms++;
        isConnected = activatedPlatforms == REQUIRED_ITEMS;
    }
    [PunRPC]
    private void OnItemsConnected()
    {
        CharacterController[] characterControllers = FindObjectsOfType<CharacterController>();
        foreach (CharacterController controller in characterControllers)
        {
            controller.isFrozen = true;
        }
        gameEndingImage.SetActive(true);
        StartCoroutine(EndGame());
        
    }
    private IEnumerator EndGame()
    {
        Color endingColor = gameEndingImage.GetComponent<Image>().color;
        CameraController[] cameras = FindObjectsOfType<CameraController>();
        foreach (CameraController camera in cameras)
        {
            StartCoroutine(camera.ShakeCamera());
        }
        while (endingColor.a < 1)
        {
            endingColor.a += 0.05f;
            yield return new WaitForSeconds(0.15f);
        }
        
        TMP_Text EndText = gameEndingImage.transform.GetChild(0).GetComponent<TMP_Text>();
        EndText.text = "The residents expelled the demons.";

    }
}
