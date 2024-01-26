using TMPro;
using UnityEngine;

public class QuestDisplayer : MonoBehaviour
{
    [SerializeField] private QuestSwitcher questSwitcher;
    private Animator questAnimator;
    private TMP_Text questText;
    private int interactedTargets = 0;
    
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerInstance");
        questAnimator = GetComponent<Animator>();
        questSwitcher = player.GetComponent<QuestSwitcher>();//FindObjectOfType<QuestSwitcher>();
        questText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        questSwitcher.onGivenQuest += DisplayNewQuest;
        questSwitcher.onPassedQuest += DestroyPassedQuest;
    }
    private void OnDisable()
    {
        questSwitcher.onGivenQuest -= DisplayNewQuest;
        questSwitcher.onPassedQuest -= DestroyPassedQuest;
    }
    private void DisplayNewQuest()
    {
        questAnimator.SetTrigger("Received");
        QuestData quest = questSwitcher.currentQuest;
        questText.text = $"{quest.description} ({interactedTargets}/{quest.requiredTargets})";
    }
    private void DestroyPassedQuest()
    {
        questAnimator.SetTrigger("Passed");
        questText.text = "";
    }
}
