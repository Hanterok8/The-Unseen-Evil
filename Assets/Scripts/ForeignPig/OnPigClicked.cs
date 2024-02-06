using System;
using UnityEngine;

public class OnPigClicked : MonoBehaviour
{
    private Camera camera;
    private const int DISTANCE = 5;
    public Action onAPigClicked;
    private void Start()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, DISTANCE) && Input.GetKeyDown(KeyCode.E))
        {
            if (hit.collider.gameObject.CompareTag("ForeignPig"))
            {
                onAPigClicked?.Invoke();
            }
        }
    }
}
