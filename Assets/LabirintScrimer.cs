using Photon.Pun;
using System.Collections;
using UnityEngine;

public class LabirintScrimer : MonoBehaviour
{
    [SerializeField] private Animator _labirintScrimer;
    [SerializeField] private GameObject _scrimer;
    [SerializeField] private GameObject _UI;
    [SerializeField] private GameObject _leftHandImage;
    [SerializeField] private GameObject _rightHandImage;
    [SerializeField] private AudioSource _scrimerSound;
    [SerializeField] private GameObject _cube;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _UI.SetActive(false);
            _leftHandImage.SetActive(true);
            _rightHandImage.SetActive(true);
            _scrimer.SetActive(true);
            _labirintScrimer.SetBool("ScrimerTrue", true);
            _scrimerSound.Play();
            StartCoroutine(CDdelete());
        }
    }
    private IEnumerator CDdelete()
    {
        yield return new WaitForSeconds(3);
        _UI.SetActive(true);
        _leftHandImage.SetActive(false);
        _rightHandImage.SetActive(false);
        _scrimer.SetActive(false);
        Destroy(_cube);
    }
}
