using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _person;
    
    private void Start()
    {
        int random = Random.Range(0, _person.Length);
        Instantiate(_person[random], transform);
    }
}
