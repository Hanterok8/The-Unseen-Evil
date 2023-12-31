using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(BloodlustSettings))]
public class OnAimodipsisMode : MonoBehaviour
{
    [SerializeField] private GameObject inhabitantPrefab;
    [SerializeField] private GameObject demonPrefab;
    [SerializeField] private PhotonView photonView;
    private BloodlustSettings bloodLust;
    void Start()
    {
        photonView = transform.GetChild(0).GetComponent<PhotonView>();
        bloodLust = GetComponent<BloodlustSettings>();
        if (!GetComponent<PlayerOrDemon>().isDemon && photonView.IsMine)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (bloodLust._demonBloodlust >= 60 && Input.GetKeyDown(KeyCode.F) && !IsAimodipsis.isAimodipsis)
        {
            RebornPlayer(demonPrefab, true);
        }
        else if (IsAimodipsis.isAimodipsis && bloodLust._demonBloodlust <= 0)
        {
            RebornPlayer(inhabitantPrefab, false);
        }
        //else if (aimodipsis.isAimodipsis && Input.GetKeyDown(KeyCode.F))
        //{
        //    RebornPlayer(demonPrefab);
        //}
    }
    private void RebornPlayer(GameObject prefabToSpawn, bool aimodipsisModeTurnTo)
    {
        GameObject spawnedPrefab = PhotonNetwork.Instantiate
            (prefabToSpawn.name, transform.GetChild(0).localPosition + Vector3.up * 3, Quaternion.identity);
        PhotonNetwork.Destroy(transform.GetChild(0).gameObject);
        spawnedPrefab.GetComponent<PlayerParent>().SetNewParent(transform);
        IsAimodipsis.isAimodipsis = aimodipsisModeTurnTo;
        photonView = spawnedPrefab.GetComponent<PhotonView>();
    }
}
