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
        animator.SetTrigger("Open");
    }

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
