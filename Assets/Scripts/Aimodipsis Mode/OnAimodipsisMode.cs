using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(BloodlustSettings))]
public class OnAimodipsisMode : MonoBehaviour
{
    [SerializeField] private GameObject inhabitantPrefab;
    [SerializeField] private GameObject demonPrefab;
    [SerializeField] private BloodlustSettings bloodLust;
    private PhotonView photonView;

    void Start()
    {
        photonView = transform.GetChild(0).GetComponent<PhotonView>();
        bloodLust = GetComponent<BloodlustSettings>();
        if (!GetComponent<PlayerOrDemon>().isDemon)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (bloodLust._demonBloodlust >= 60 && Input.GetKeyDown(KeyCode.F) && !bloodLust.isAimodipsis)
        {
            RebornPlayer(demonPrefab);
        }
        else if (bloodLust.isAimodipsis && bloodLust._demonBloodlust <= 0)
        {
            RebornPlayer(inhabitantPrefab);  
        }
        else
        {
            return;
        }
        PhotonNetwork.Destroy(transform.GetChild(0).gameObject);
        photonView = transform.GetChild(0).GetComponent<PhotonView>();
        bloodLust = GetComponent<BloodlustSettings>();
    }
    private void RebornPlayer(GameObject instantiationPrefab)
    {
        GameObject prefabInstantiation = PhotonNetwork.Instantiate(instantiationPrefab.name, transform.GetChild(0).position, Quaternion.identity);
        prefabInstantiation.transform.parent = transform;
        bloodLust.isAimodipsis = !bloodLust.isAimodipsis;
    }
}
