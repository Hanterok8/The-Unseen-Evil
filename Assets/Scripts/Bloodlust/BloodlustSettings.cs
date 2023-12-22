using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodlustSettings : MonoBehaviourPunCallbacks
{
    public bool isAimodipsis;
    private PhotonView _photonView;
    public int _demonBloodlust;
    private void Start()
    {
        _demonBloodlust = 0;
        _photonView = GetComponent<PhotonView>();
        StartCoroutine(BloodlustScale());
    }
    private void Update()
    {
        ActivateAimodipsis();
        if (isAimodipsis && _demonBloodlust <= 0)
        {
            isAimodipsis = false;
        }
    }
    private void ActivateAimodipsis()
    {
        if (_demonBloodlust >= 60 && Input.GetKeyDown(KeyCode.F) && !isAimodipsis)
        {
            isAimodipsis = true;
        }

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
