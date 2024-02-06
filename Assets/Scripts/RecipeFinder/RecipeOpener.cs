using System;
using UnityEngine;

public class RecipeOpener : MonoBehaviour
{
    private Camera camera;
    private const int DISTANCE = 5;
    [SerializeField] private LayerMask layerMask;
    public Action onEnabledRecipeUI;
    private void Start()
    {
        camera = Camera.main;
    }
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
