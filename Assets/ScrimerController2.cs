using System.Collections;
using Photon.Pun;
using UnityEngine;

public class ScrimerController2 : MonoBehaviour
{
    [SerializeField] private AudioSource _sourse;
    [SerializeField] private Animator _scrimerAnim;
    [SerializeField] private Light[] _light;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private GameObject _cube;
    private PhotonView photonView;

    private void Start() => photonView = GetComponent<PhotonView>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && photonView.IsMine)
        {
            StartCoroutine(ScrimerCD());
            foreach (Light light in _light)
            {
                light.enabled = false;
            }
            _sourse.Play();
        }
    }
    private IEnumerator ScrimerCD()
    {
        _scrimerAnim.SetBool("isTrrigerScrim", true);
        yield return new WaitForSeconds(1);
        _scrimerAnim.SetBool("isTrrigerScrim", false);
        Destroy(_gameObject,1);
        yield return new WaitForSeconds(3);
        foreach (Light light in _light)
        {
            light.enabled = true;
        }
        if (photonView.IsMine) Destroy(_cube);
    }
}
