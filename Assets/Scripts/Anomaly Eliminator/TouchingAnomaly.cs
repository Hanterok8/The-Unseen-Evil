using System;
using UnityEngine;

public class TouchingAnomaly : MonoBehaviour
{
    public Action<Collider> onAnomalyTouched;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Anomaly"))
        {
            onAnomalyTouched?.Invoke(collider);
            Destroy(collider);
        }
    }
}
