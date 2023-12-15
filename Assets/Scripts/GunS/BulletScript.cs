using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 direction;
    private float range;
    private LayerMask whatIsEnemy;

    public void SetupBullet(Vector3 _direction, float _range, LayerMask _whatIsEnemy)
    {
        direction = _direction;
        range = _range;
        whatIsEnemy = _whatIsEnemy;

        transform.position = Vector3.zero;

        StartCoroutine(MoveBullet());
    }

    IEnumerator MoveBullet()
    {
        float traveledDistance = 0f;

        while (traveledDistance < range)
        {
            transform.Translate(direction.normalized * Time.deltaTime * 50f);
            traveledDistance += Time.deltaTime * 50f;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, 0.1f, whatIsEnemy))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    //hit.collider.GetComponent<Enemy>().TakeDamage(damage);
                }
                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }

        
        Destroy(gameObject);
    }
}
