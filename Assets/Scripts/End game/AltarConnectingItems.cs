using Photon.Pun;
using System.Collections;
using ExitGames.Client.Photon.StructWrapping;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AltarConnectingItems : MonoBehaviour
{
    private const int ALL_GAME_QUESTS = 9;
    
    [SerializeField] private GameObject gameEndingImage;
    private GameObject lastInteractedPlayer;
    [SerializeField] private bool isConnected = false;
    [SerializeField] private int activatedPlatforms = 0;
    private const int REQUIRED_ITEMS = 2;
    private TotalCompletedQuestsCounter totalQuests;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        totalQuests = FindObjectOfType<TotalCompletedQuestsCounter>();
    }
    public void AddActivatedPlatform(GameObject player)
    {
        lastInteractedPlayer = player;
        photonView.RPC(nameof(AddActivatedPlatformRPC), RpcTarget.All);
    }
    
    [PunRPC]
    private void AddActivatedPlatformRPC()
    {
        activatedPlatforms++;
        isConnected = activatedPlatforms == REQUIRED_ITEMS;
        if (isConnected)
            Invoke(nameof(OnItemsConnected), 1f);
    }

    private void OnItemsConnected()
    {
        Debug.Log("Items connected");
        photonView.RPC(nameof(OnItemsConnectedRPC), RpcTarget.All);
        Invoke(nameof(MoveToLobby), 7);
    }
    [PunRPC]
    private void OnItemsConnectedRPC()
    {
        int neededNumber = (int)(PhotonNetwork.PlayerList.Length * (float)(ALL_GAME_QUESTS / 2));
        Debug.Log((totalQuests.TotalQuestsDone < neededNumber) + "\n" + totalQuests.TotalQuestsDone + " " + neededNumber);
        if (totalQuests.TotalQuestsDone < neededNumber) return;
        QuestSwitcher questSwitcher = lastInteractedPlayer.GetComponent<QuestSwitcher>();
        Debug.Log("Success");
        int i = 0;
        foreach (QuestData quest in questSwitcher.completedQuest)
        {
            if (quest.isQuestRequiredForEndingGame)
            {
                i++;
            }
        }

        Debug.Log(i + " " + (i < 4));
        if (i < 4) return;
        CharacterController[] characterControllers = FindObjectsOfType<CharacterController>();
        foreach (CharacterController controller in characterControllers)
        {
            controller.isFrozen = true;
        }
        gameEndingImage.SetActive(true);
        
        // CameraController[] cameras = FindObjectsOfType<CameraController>();
        // foreach (CameraController camera in cameras)
        // {
        //     StartCoroutine(camera.ShakeCamera());
        // }
        TMP_Text EndText = gameEndingImage.transform.GetChild(0).GetComponent<TMP_Text>();
        EndText.text = "The residents expelled the demon.";
        
    }

    private void MoveToLobby()
    {
        PhotonNetwork.LoadLevel("Menu");
    }

}
