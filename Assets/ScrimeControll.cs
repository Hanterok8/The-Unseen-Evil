using System.Collections;
using UnityEngine;

public class ScrimeControll : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private Light[] _lights;
    [SerializeField] private AudioClip _scrimer;
    [SerializeField] private AudioSource _scrimerSourse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _scrimerSourse.Play();
            foreach (Light light in _lights)
            {
                light.color = Color.red;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(Culdayn());
    }
    private IEnumerator Culdayn()
    {
        yield return new WaitForSeconds(5);    
        Destroy(_cube);
        foreach (Light light in _lights)
        {
            light.color = Color.white;
        }
    }
}
