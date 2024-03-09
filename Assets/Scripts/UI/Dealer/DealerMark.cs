using UnityEngine;
using TMPro;

public class DealerMark : MonoBehaviour
{
    [SerializeField] private float coefficient;
    [SerializeField] private TMP_Text metersToDealer;
    private Transform camera;
    private Transform DealerImage;
    
    void Start()
    {
        camera = Camera.main.transform;
        DealerImage = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, camera.position);
        float newScale = distanceToPlayer * coefficient / 100;
        DealerImage.localScale = new Vector3(newScale, newScale, newScale);
        metersToDealer.text = $"{(int)distanceToPlayer} m";
        transform.LookAt(camera);
        if (distanceToPlayer < 15 && DealerImage.gameObject.activeSelf)
        {
            DealerImage.gameObject.SetActive(false);
        }
        else if (distanceToPlayer >= 15 && !DealerImage.gameObject.activeSelf)
        {
            DealerImage.gameObject.SetActive(true);
        }
    }
}
