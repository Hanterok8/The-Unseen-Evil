using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;

[RequireComponent(typeof(BloodlustSettings))]
public class OnAimodipsisMode : MonoBehaviour
{
    [SerializeField] private GameObject residentPrefab;
    [SerializeField] private GameObject demonPrefab;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private AudioClip _demonSound;
    [SerializeField] private AudioClip _demonVoplSound;
    [SerializeField] private AudioSource _demonSource;
    [SerializeField] private AudioSource _demonVoplSource;
    private CurrentPlayer currentLivingPlayer;
    private BloodlustSettings bloodLust;
    private bool isPlayerBecomeDemon;
    private IsAimodipsis isAimodipsisMode;

    void Start()
    {
        isAimodipsisMode = GetComponent<IsAimodipsis>();
        currentLivingPlayer = GetComponent<CurrentPlayer>();
        photonView = GetComponent<PhotonView>();
        //photonView = currentLivingPlayer.CurrentPlayerModel.GetComponent<PhotonView>();
        bloodLust = GetComponent<BloodlustSettings>();
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (bloodLust._demonBloodlust >= 60 && Input.GetKeyDown(KeyCode.F) && !isAimodipsisMode.isAimodipsis)
        {
            RebornPlayer(demonPrefab, true);
            _demonSource.PlayOneShot(_demonSound);
        }
        else if (isAimodipsisMode.isAimodipsis && bloodLust._demonBloodlust <= 0)
        {
            RebornPlayer(residentPrefab, false);
            _demonVoplSource.PlayOneShot(_demonVoplSound);
        }
    }
    private void RebornPlayer(GameObject prefabToSpawn, bool aimodipsisModeTurnTo)
    {
        GameObject spawnedPrefab = PhotonNetwork.Instantiate
            (prefabToSpawn.name, currentLivingPlayer.CurrentPlayerModel.transform.position + Vector3.up, Quaternion.identity);
        PhotonNetwork.Destroy(currentLivingPlayer.CurrentPlayerModel);
        isAimodipsisMode.SetAimodipsisMode(aimodipsisModeTurnTo);
        currentLivingPlayer.CurrentPlayerModel = spawnedPrefab;
        photonView = GetComponent<PhotonView>();
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
    // [PunRPC]
    // private void TurnVoiceChatIntoRPC(bool voiceChatTurnInto)
    // {
    //     Speaker[] speakers = FindObjectsOfType<Speaker>();
    //
    //     foreach (Speaker speaker in speakers)
    //     {
    //         speaker.enabled = voiceChatTurnInto;
    //     }
    // }

}
