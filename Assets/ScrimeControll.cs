using System.Collections;
using UnityEngine;
using Photon.Pun;

public class ScrimeControll : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private Light[] _lights;
    [SerializeField] private AudioClip _scrimer;
    [SerializeField] private AudioSource _scrimerSourse;
    private PhotonView photonView;

    private void Start() => photonView = GetComponent<PhotonView>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<PhotonView>().Owner.NickName == photonView.Owner.NickName && photonView.IsMine)
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
        if (other.gameObject.CompareTag("Player") && other.GetComponent<PhotonView>().Owner.NickName == photonView.Owner.NickName && photonView.IsMine)
        {
            StartCoroutine(Culdayn());
        }
         
    }
    private IEnumerator Culdayn()
    {
        Destroy(gameObject);
        yield return new WaitForSeconds(5);    
        if (photonView.IsMine) Destroy(_cube);
        foreach (Light light in _lights)
        {
            light.color = Color.white;
        }
    }
}
