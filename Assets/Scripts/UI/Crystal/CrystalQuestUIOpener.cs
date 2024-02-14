using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalQuestUIOpener : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Weapon weapon;
    private CrystalElements crystalElements;

    private void Start()
    {
        crystalElements = FindObjectOfType<CrystalElements>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            OpenCrystalUI();
        }
    }

    private void OpenCrystalUI()
    {
        cameraController.enabled = false;
        weapon.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        GameObject crystalCanvas = crystalElements.crystalCanvas;
        crystalCanvas.SetActive(true);
        GameObject blur = crystalElements.blur;
        blur.SetActive(true);
    }
}
