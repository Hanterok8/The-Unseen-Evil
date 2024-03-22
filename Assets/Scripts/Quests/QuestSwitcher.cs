using Photon.Pun;
using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class QuestSwitcher : MonoBehaviourPunCallbacks
{
    private const int SECONDS_ON_START = 7;
    private const int DELAY = 1;

    public Action onGivenQuest;
    public Action onPassedQuest;
    public Action<int> onReceivedCoinsByQuest;
    public Action onAllQuestsCompleted;
    public Action onAddedQuestStep;
    public List<QuestData> completedQuest = new List<QuestData>();
    public QuestData currentQuest;
    public int interactedTargets = 0;
    
    [SerializeField] public List<QuestData> leftQuests = new List<QuestData>();
    [Tooltip("The quest that requires some items in order to complete it.")] 
    [SerializeField] public QuestData extraQuest;
    [SerializeField] private int secondsBeforeNextQuest;

    private TotalCompletedQuestsCounter totalQuestsCounter;
    private PhotonView photonView;
    private bool canQuestBeTaken;

    private void Start()
    {
        totalQuestsCounter = FindObjectOfType<TotalCompletedQuestsCounter>();
        photonView = GetComponent<PhotonView>();
        secondsBeforeNextQuest = SECONDS_ON_START;
        if (!photonView.IsMine) return;
        InvokeRepeating(nameof(ReloadQuest), 1, DELAY);
    }
    private void ReloadQuest()
    {
        if (secondsBeforeNextQuest > 0)
        {
            secondsBeforeNextQuest--;
        }
        if (secondsBeforeNextQuest <= 0 && currentQuest == null)
        {
            GiveNewQuest();
        }
    }
    public void PassQuest(int receivedCoins)
    {
        totalQuestsCounter.UpdateTotalQuestsCount(1);
        completedQuest.Add(currentQuest);
        currentQuest = null; 
        onPassedQuest?.Invoke();
        onReceivedCoinsByQuest?.Invoke(receivedCoins);
        if (leftQuests.Count == 0)
        {
            onAllQuestsCompleted?.Invoke();
        }
    }
    private void GiveNewQuest()
    {
        if (leftQuests.Count == 0)
        {
            return;
        }
        interactedTargets = 0;
        secondsBeforeNextQuest = SECONDS_ON_START;
        int randomQuest = UnityEngine.Random.Range(0, leftQuests.Count);
        currentQuest = leftQuests[randomQuest];
        leftQuests.Remove(leftQuests[randomQuest]);
        onGivenQuest?.Invoke();
    }
    public void AddQuestStep(int steps)
    {
        interactedTargets += steps;
        onAddedQuestStep?.Invoke();
        if (interactedTargets == currentQuest.requiredTargets)
        {
            PassQuest(currentQuest.coinsForQuest);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        totalQuestsCounter.UpdateTotalQuestsCount(-completedQuest.Count);
    }
}
