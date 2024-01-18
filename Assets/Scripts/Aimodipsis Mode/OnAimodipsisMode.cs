using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;

[RequireComponent(typeof(BloodlustSettings))]
public class OnAimodipsisMode : MonoBehaviour
{
    [SerializeField] private GameObject residentPrefab;
    [SerializeField] private GameObject demonPrefab;
    [SerializeField] private PhotonView photonView;

    private CurrentPlayer currentLivingPlayer;
    private BloodlustSettings bloodLust;

    //private VoiceModeChanger voiceModeChanger;

    private bool isPlayerBecomeDemon;

    private IsAimodipsis isAimodipsisMode;
    void Start()
    {

        isAimodipsisMode = GetComponent<IsAimodipsis>();
        //voiceModeChanger = FindObjectOfType<VoiceModeChanger>();
        currentLivingPlayer = FindObjectOfType<CurrentPlayer>();
        photonView = currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        bloodLust = GetComponent<BloodlustSettings>();
        if (!GetComponent<PlayerOrDemon>().isDemon && photonView.IsMine)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if(photonView == null) photonView = currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        if (!photonView.IsMine) return;

        if (bloodLust._demonBloodlust >= 60 && Input.GetKeyDown(KeyCode.F) && !isAimodipsisMode.isAimodipsis)
        {
            RebornPlayer(demonPrefab, true);
            //photonView.RPC(nameof(TurnOnAimodipsisForAllPlayers), RpcTarget.All);
        }
        else if (isAimodipsisMode.isAimodipsis && bloodLust._demonBloodlust <= 0)
        {
            RebornPlayer(residentPrefab, false);
        }
    }
    private void RebornPlayer(GameObject prefabToSpawn, bool aimodipsisModeTurnTo)
    {
        GameObject spawnedPrefab = PhotonNetwork.Instantiate
            (prefabToSpawn.name, currentLivingPlayer.CurrentPlayerModel.transform.localPosition + Vector3.up * 3, Quaternion.identity);
        PhotonNetwork.Destroy(currentLivingPlayer.CurrentPlayerModel);
        isAimodipsisMode.isAimodipsis = aimodipsisModeTurnTo;
        currentLivingPlayer.CurrentPlayerModel = spawnedPrefab;
        photonView = currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        photonView.RPC(nameof(TurnVoiceChatIntoRPC), RpcTarget.All, aimodipsisModeTurnTo);
        photonView.RPC(nameof(TurnOnAimodipsisForAllPlayers), RpcTarget.All, aimodipsisModeTurnTo);
        isPlayerBecomeDemon = aimodipsisModeTurnTo;
    }
    [PunRPC]
    private void TurnOnAimodipsisForAllPlayers(bool setAimodipsisModeTo)
    {
        IsAimodipsis[] isAimodipsisses = FindObjectsOfType<IsAimodipsis>();
        for (int i = 0; i < isAimodipsisses.Length; i++)
        {
            isAimodipsisses[i].SetAimodipsisMode(setAimodipsisModeTo);
        }
    }
    [PunRPC]
    private void TurnVoiceChatIntoRPC(bool voiceChatTurnInto)
    {
        Speaker[] speakers = FindObjectsOfType<Speaker>();

        foreach (Speaker speaker in speakers)
        {
            speaker.enabled = voiceChatTurnInto;
        }
    }

}
