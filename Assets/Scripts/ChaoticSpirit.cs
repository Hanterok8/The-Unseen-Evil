using UnityEngine;

public class ChaoticSpirit : MonoBehaviour
{
    public float speed = 5f;
    public float changeDirectionInterval = 2f;

    private Vector3 randomDirection;
    private float timer;
    private Collider mapCollider;

    void Start()
    {
        SetRandomDirection();

        // ѕолучаем Collider карты (убедитесь, что у карты есть Collider)
        mapCollider = GameObject.FindGameObjectWithTag("Map").GetComponent<Collider>();

        if (mapCollider == null)
        {
            Debug.LogError("Map Collider not found.");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeDirectionInterval)
        {
            SetRandomDirection();
            timer = 0f;
        }

        Move();
        ClampPositionToBounds();
    }

    void SetRandomDirection()
    {
        randomDirection = Random.onUnitSphere.normalized;
    }

    void Move()
    {
        Vector3 newPosition = transform.position + randomDirection * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    void ClampPositionToBounds()
    {
        if (mapCollider == null)
        {
            Debug.LogError("Map Collider not found.");
            return;
        }

        // ќграничиваем положение духа внутри Collider карты
        if (!mapCollider.bounds.Contains(transform.position))
        {
            // ≈сли дух покидает границы карты, устанавливаем его обратно внутри
            Vector3 clampedPosition = mapCollider.ClosestPoint(transform.position);
            transform.position = clampedPosition;
        }
    }

    // ѕровер€ем столкновение с коллайдером Player
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // ≈сли дух касаетс€ коллайдера Player, удал€ем дух
            Destroy(gameObject);
        }
    }
}
