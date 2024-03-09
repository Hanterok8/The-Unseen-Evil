using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SmoothAnimationActivator : MonoBehaviour
{
    private Animator animator;
    

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.ResetTrigger("Close");
        animator.SetTrigger("Open");
        gameObject.SetActive(true);
    }

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
