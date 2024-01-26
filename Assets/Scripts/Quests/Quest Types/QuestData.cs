using UnityEngine;

[CreateAssetMenu(fileName = "newQuestData", menuName = "Quest Data")]
public class QuestData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _requiredTargets;
    public string name => _name;
    public string description => _description;
    public int requiredTargets => _requiredTargets;

}
