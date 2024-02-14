using UnityEngine;

public class DisablePlayerCanvas : MonoBehaviour
{
    private CharacterController characterController;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        characterController = player.GetComponent<CharacterController>();
       // characterController.onTransformedToSpectator += DisableCanvas;
    }
    private void OnDisable()
    {
        //characterController.onTransformedToSpectator -= DisableCanvas;
    }
    public virtual void DisableCanvas()
    {
        gameObject.SetActive(false);
    }
}
