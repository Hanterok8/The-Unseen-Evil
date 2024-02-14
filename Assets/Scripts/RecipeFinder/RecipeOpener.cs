using System;
using UnityEngine;

public class RecipeOpener : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask layerMask;
    private const int DISTANCE = 5;
    public Action onEnabledRecipeUI;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, DISTANCE, layerMask) && Input.GetKeyDown(KeyCode.E))
        {
            onEnabledRecipeUI?.Invoke();  
        }
    }
}
