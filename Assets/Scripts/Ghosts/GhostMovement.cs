using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 7f;
    [SerializeField] private List<Transform> MaydeTargets = new List<Transform>();
    [SerializeField] private Transform TargetObject;
    private string TargetObjectName;

    private void Start()
    {
        RandomTarget();
    }

    private void Update()
    {
        MoveTo();
    }

    private void MoveTo()
    {
        if (TargetObject != null)
        {
            Vector3 direction = TargetObject.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * Speed * Time.deltaTime);
        }
    }

    private void RandomTarget()
    {
        int randomIndex = Random.Range(0, MaydeTargets.Count);
        if (MaydeTargets.Count > 1)
        {
            int lastIndex = randomIndex;
            while (randomIndex == lastIndex)
            {
                randomIndex = Random.Range(0, MaydeTargets.Count);
            }
        }
        TargetObject = MaydeTargets[randomIndex];
        TargetObjectName = TargetObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        string colider_name = other.gameObject.name;
        if (colider_name == TargetObjectName)
        {
            RandomTarget();
        }
    }
}
