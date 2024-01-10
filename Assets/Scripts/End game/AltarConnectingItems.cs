using TMPro;
using UnityEngine;

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
    private void OnItemsConnected()
    {
        gameEndingImage.SetActive(true);
        TMP_Text EndText = gameEndingImage.transform.GetChild(0).GetComponent<TMP_Text>();
        EndText.text = "The residents expelled the demons.";
    }
}
