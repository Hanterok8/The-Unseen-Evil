using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingAnomaly : MonoBehaviour
{
    public Action onAnomalyTouched;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Anomaly"))
        {
            collider.GetComponent<ParticleSystem>().loop = false;
            onAnomalyTouched?.Invoke();
        }
    }
}
