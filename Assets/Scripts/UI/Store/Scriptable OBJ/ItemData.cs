using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private int _price;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    public int price => this._price;
    public string name => this._name;
    public Sprite sprite => this._sprite;

}
