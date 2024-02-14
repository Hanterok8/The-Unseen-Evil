using UnityEngine;

public class DisablePlayerCanvas : MonoBehaviour
{
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
        gameObject.SetActive(false);
    }
}
