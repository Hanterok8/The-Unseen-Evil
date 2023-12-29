using Photon.Pun;
using System.Collections;
using UnityEngine;

public class BloodlustSettings : MonoBehaviourPunCallbacks
{
    public int _demonBloodlust;
    private IsAimodipsis aimodipsis;
    private PhotonView _photonView;
    private void Start()
    {
        if (!GetComponent<PlayerOrDemon>().isDemon)
        {
            enabled = false;
            return;
        }
        aimodipsis = Object.FindFirstObjectByType<IsAimodipsis>();
        _demonBloodlust = 0;
        _photonView = transform.GetChild(0).GetComponent<PhotonView>();
        StartCoroutine(BloodlustScale());
    }
    void Update()
    {
        if(_photonView == null) _photonView = transform.GetChild(0).GetComponent<PhotonView>();
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
            if (!aimodipsis.isAimodipsis)
            {
                if(_demonBloodlust < 100) _demonBloodlust += 50;
                yield return new WaitForSeconds(3);
            }
            else
            {
                if(_demonBloodlust > 0) _demonBloodlust -= 1;
                yield return new WaitForSeconds(1);
            }
        }
    }
}
