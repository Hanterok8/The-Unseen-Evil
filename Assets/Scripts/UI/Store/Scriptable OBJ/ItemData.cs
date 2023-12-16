using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _price;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    public string price => this._price;
    public string name => this._name;
    public Sprite sprite => this._sprite;

}
