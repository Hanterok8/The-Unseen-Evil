using TMPro;
using UnityEngine;

public class AltarConnectingItems : MonoBehaviour
{
    [SerializeField] private GameObject gameEndingImage;
    private bool isConnected = false;
    private int activatedPlatforms = 0;
    private void Update()
    {
        if (isConnected)
            Invoke(nameof(OnItemsConnected), 1f);
    }
    public void AddActivatedPlatform()
    {
        activatedPlatforms++;
        isConnected = activatedPlatforms == 1;
    }
    private void OnItemsConnected()
    {
        gameEndingImage.SetActive(true);
        TMP_Text EndText = gameEndingImage.transform.GetChild(0).GetComponent<TMP_Text>();
        EndText.text = "The residents expelled the demons.";
    }
}
