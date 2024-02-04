using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestSwitcher : MonoBehaviour
{
    [SerializeField] private List<QuestData> leftQuests = new List<QuestData>();
    [SerializeField] private int secondsBeforeNextQuest;
    public List<QuestData> completedQuest = new List<QuestData>();
    public QuestData currentQuest;
    public Action onGivenQuest;
    public Action onPassedQuest;
    public Action<int> onReceivedCoinsByQuest;
    public Action onAllQuestsCompleted;
    public Action onAddedQuestStep;

    public int interactedTargets = 0;
    private bool canQuestBeTaken;
    private PhotonView photonView;
    private const int SECONDS_ON_START = 7;
    private const int DELAY = 1;

    private void Start()
    {
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
        completedQuest.Add(currentQuest);
        currentQuest = null;
        onPassedQuest?.Invoke();
        onReceivedCoinsByQuest?.Invoke(receivedCoins);
    }
    private void GiveNewQuest()
    {
        if (leftQuests.Count == 0)
        {
            onAllQuestsCompleted?.Invoke();
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
    }

}
