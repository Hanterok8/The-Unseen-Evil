using System;
using Photon.Pun;
using UnityEngine;

public class OnPigClicked : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private const int DISTANCE = 5;
    public Action onAPigClicked;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (!photonView.IsMine) return;
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, DISTANCE) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("RAYCAST TO PIG - " + hit.collider.gameObject.CompareTag("ForeignPig"));
            if (hit.collider.gameObject.CompareTag("ForeignPig"))
            {
                onAPigClicked?.Invoke();
            }
        }
    }
}
