using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private List<Transform> maydeTargets = new List<Transform>();
    [SerializeField] private Transform targetObject;
    private string targetObjectName;

    private void Start()
    {
        RandomeTarget();
    }

    private void Update()
    {
        MoveTo();
    }

    private void MoveTo()
    {
        if (targetObject != null)
        {
            Vector3 direction = targetObject.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void RandomeTarget()
    {
        int randomIndex = Random.Range(0, maydeTargets.Count);
        if (maydeTargets.Count > 1)
        {
            int lastIndex = randomIndex;
            while (randomIndex == lastIndex)
            {
                randomIndex = Random.Range(0, maydeTargets.Count);
            }
        }
        targetObject = maydeTargets[randomIndex];
        targetObjectName = targetObject.name;
    }

    private void OnTriggerStay(Collider other)
    {
        string colider_name = other.gameObject.name;
        if (colider_name == targetObjectName)
        {
            RandomeTarget();
        }
    }
}
