using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrystalMover : MonoBehaviour
{
    [SerializeField] private GameObject staticPositionedElement;
    private int crystalIndex;
    private bool isMouseButtonPressed = false;
    private static bool isCursorBusy = false;
    private bool isMouseAimedToElementPosition = false;

    private void OnEnable()
    {
        crystalIndex = GetComponent<CrystalIndex>().elementIndex;
    }
    private void Update()
    {
        if (isMouseButtonPressed) 
            transform.position = Input.mousePosition;
    }
    public void OnMousePressedDown()
    {
        if (isCursorBusy) return;
        isMouseButtonPressed = true;
        isCursorBusy = true;
    }
    public void OnMousePressedUp()
    {
        isMouseButtonPressed = false;
        isCursorBusy = false;
        if (isMouseAimedToElementPosition)
        {
            staticPositionedElement.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<CrystalIndex>().elementIndex == crystalIndex)
        {
            isMouseAimedToElementPosition = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        isMouseAimedToElementPosition = false;
    }
}
