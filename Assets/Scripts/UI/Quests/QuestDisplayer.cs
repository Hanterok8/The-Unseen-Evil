using TMPro;
using UnityEngine;

public class QuestDisplayer : MonoBehaviour
{
    [SerializeField] private QuestSwitcher questSwitcher;
    private Animator questAnimator;
    private TMP_Text questText;
    
    private void Awake()
    {
        questAnimator = GetComponent<Animator>();
        questText = GetComponent<TMP_Text>();
    }
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerInstance");
        questSwitcher = player.GetComponent<QuestSwitcher>();
        questSwitcher.onGivenQuest += DisplayNewQuest;
        questSwitcher.onPassedQuest += DestroyPassedQuest;
        questSwitcher.onAllQuestsCompleted += DisplayNoQuests;
        questSwitcher.onAddedQuestStep += UpdateQuestState;
    }
    private void OnDisable()
    {
        questSwitcher.onGivenQuest -= DisplayNewQuest;
        questSwitcher.onPassedQuest -= DestroyPassedQuest;
        questSwitcher.onAllQuestsCompleted -= DisplayNoQuests;
        questSwitcher.onAddedQuestStep -= UpdateQuestState;
    }
    private void DisplayNoQuests()
    {
        questText.text = $"You have completed all the quests.";
    }
    private void DisplayNewQuest()
    {
        questAnimator.SetTrigger("Received");
        UpdateQuestState();
    }
    private void UpdateQuestState()
    {
        QuestData quest = questSwitcher.currentQuest;
        questText.text = $"{quest.description} ({questSwitcher.interactedTargets}/{quest.requiredTargets})";
    }
    private void DestroyPassedQuest()
    {
        questAnimator.SetTrigger("Passed");
        questText.text = "";
    }
}
