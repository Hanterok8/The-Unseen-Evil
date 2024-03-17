using System.Collections;
using UnityEngine;

public class ScrimerController2 : MonoBehaviour
{
    [SerializeField] private Animator _scrimerAnim;
    [SerializeField] private GameObject _gameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ScrimerCD());
        }
    }
    private IEnumerator ScrimerCD()
    {
        _scrimerAnim.SetBool("isTrrigerScrim", true);
        yield return new WaitForSeconds(1);
        _scrimerAnim.SetBool("isTrrigerScrim", false);
        Destroy(_gameObject,2);
    }
}
