using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(BloodlustSettings))]
public class OnAimodipsisMode : MonoBehaviour
{
    [SerializeField] private GameObject inhabitantPrefab;
    [SerializeField] private GameObject demonPrefab;
    [SerializeField] private BloodlustSettings bloodLust;
    private IsAimodipsis aimodipsis;
    private PhotonView photonView;

    void Start()
    {
        aimodipsis = Object.FindFirstObjectByType<IsAimodipsis>();
        photonView = transform.GetChild(0).GetComponent<PhotonView>();
        bloodLust = GetComponent<BloodlustSettings>();
        if (!GetComponent<PlayerOrDemon>().isDemon && photonView.IsMine)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (bloodLust._demonBloodlust >= 60 && Input.GetKeyDown(KeyCode.F) && !aimodipsis.isAimodipsis)
        {
            RebornPlayer(demonPrefab);
        }
        if (aimodipsis.isAimodipsis && bloodLust._demonBloodlust <= 0)
        {
            RebornPlayer(inhabitantPrefab);
        }
        else if (aimodipsis.isAimodipsis && Input.GetKeyDown(KeyCode.F))
        {
            RebornPlayer(demonPrefab);
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
        aimodipsis.isAimodipsis = !aimodipsis.isAimodipsis;
    }
}
