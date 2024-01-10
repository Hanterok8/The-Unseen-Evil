using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(BloodlustSettings))]
public class OnAimodipsisMode : MonoBehaviour
{
    [SerializeField] private GameObject residentPrefab;
    [SerializeField] private GameObject demonPrefab;
    [SerializeField] private PhotonView photonView;
    private CurrentPlayer currentLivingPlayer;
    private BloodlustSettings bloodLust;

    private bool isPlayerBecomeDemon;
    void Start()
    {
        currentLivingPlayer = Object.FindObjectOfType<CurrentPlayer>();
        photonView = currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
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
            isPlayerBecomeDemon = true;
        }
        else if (IsAimodipsis.isAimodipsis && bloodLust._demonBloodlust <= 0)
        {
            RebornPlayer(residentPrefab, false);
            isPlayerBecomeDemon = false;
        }
        else if (IsAimodipsis.isAimodipsis && Input.GetKeyDown(KeyCode.F) && !isPlayerBecomeDemon)
        {
            RebornPlayer(demonPrefab, true);
        }
    }
    private void RebornPlayer(GameObject prefabToSpawn, bool aimodipsisModeTurnTo)
    {
        GameObject spawnedPrefab = PhotonNetwork.Instantiate
            (prefabToSpawn.name, currentLivingPlayer.CurrentPlayerModel.transform.localPosition + Vector3.up * 3, Quaternion.identity);
        PhotonNetwork.Destroy(currentLivingPlayer.CurrentPlayerModel);
        IsAimodipsis.isAimodipsis = aimodipsisModeTurnTo;
        photonView = spawnedPrefab.GetComponent<PhotonView>();
        currentLivingPlayer.CurrentPlayerModel = spawnedPrefab;
        
    }
}
