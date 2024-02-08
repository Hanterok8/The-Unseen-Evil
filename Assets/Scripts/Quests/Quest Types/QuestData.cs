using UnityEngine;

[CreateAssetMenu(fileName = "newQuestData", menuName = "Quest Data")]
public class QuestData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _requiredTargets;
    [SerializeField] private string[] _usingItems;
    [SerializeField] private int _coinsForQuest;
    public string name => _name;
    public string description => _description;
    public int requiredTargets => _requiredTargets;
    public string[] usingItems => _usingItems;
    public int coinsForQuest => _coinsForQuest;

}
