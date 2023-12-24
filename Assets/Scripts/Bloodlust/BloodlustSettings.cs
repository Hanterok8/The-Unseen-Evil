using Photon.Pun;
using System.Collections;
using UnityEngine;

public class BloodlustSettings : MonoBehaviourPunCallbacks
{
    public bool isAimodipsis;
    private PhotonView _photonView;
    public int _demonBloodlust;
    private void Start()
    {
        if (!GetComponent<PlayerOrDemon>().isDemon)
        {
            enabled = false;
            return;
        }
        _demonBloodlust = 0;
        _photonView = GetComponent<PhotonView>();
        StartCoroutine(BloodlustScale());
    }
    private IEnumerator BloodlustScale()
    {
        while (true)
        {
            if (!_photonView.IsMine)
            {
                StartCoroutine(BloodlustScale());
                break;
            }
            if (!isAimodipsis)
            {
                _demonBloodlust += 1;
                yield return new WaitForSeconds(3);
            }
            else
            {
                _demonBloodlust -= 1;
                yield return new WaitForSeconds(1);
            }
        }
    }
}
