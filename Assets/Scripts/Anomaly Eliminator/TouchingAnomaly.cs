using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingAnomaly : MonoBehaviour
{
    public Action<Collider> onAnomalyTouched;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Anomaly"))
        {
            
            onAnomalyTouched?.Invoke(collider);
        }
    }
}
