using UnityEngine;

public class DisablePlayerCanvas : MonoBehaviour
{
    private PersonController personController;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        personController = player.GetComponent<PersonController>();
        personController.onTransformedToSpectator += DisableCanvas;
    }
    private void OnDisable()
    {
        personController.onTransformedToSpectator -= DisableCanvas;
    }
    public virtual void DisableCanvas()
    {
        gameObject.SetActive(false);
    }
}
