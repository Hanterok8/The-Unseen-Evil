using UnityEngine;

public class DisablePlayerCanvas : MonoBehaviour
{
    [SerializeField] private GameObject[] Canvases;
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
        foreach (GameObject canvas in Canvases)
        {
            canvas.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
