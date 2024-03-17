using System.Collections;
using UnityEngine;

public class ScrimerController2 : MonoBehaviour
{
    [SerializeField] private AudioSource _sourse;
    [SerializeField] private Animator _scrimerAnim;
    [SerializeField] private Light _light;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private GameObject _cube;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ScrimerCD());
            _light.enabled = false;
            _sourse.Play();
        }
    }
    private IEnumerator ScrimerCD()
    {
        _scrimerAnim.SetBool("isTrrigerScrim", true);
        yield return new WaitForSeconds(1);
        _scrimerAnim.SetBool("isTrrigerScrim", false);
        Destroy(_gameObject,2);
        yield return new WaitForSeconds(3);
        _light.enabled = true;
        Destroy(_cube);
    }
}
