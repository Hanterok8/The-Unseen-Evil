using UnityEngine;

public class DisablePlayerCanvas : MonoBehaviour
{
    public GameObject questUI;
    private PlayerSetter playerSettings;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerSettings = player.GetComponent<PlayerSetter>();
        playerSettings.onTransformedToSpectator += DisableCanvas;
    }
    private void OnDisable()
    {
        playerSettings.onTransformedToSpectator -= DisableCanvas;
    }
    public virtual void DisableCanvas()
    {
        questUI.SetActive(false);
        gameObject.SetActive(false);
    }
}
