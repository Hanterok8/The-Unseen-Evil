using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAimodipsisMode : MonoBehaviour
{
    [SerializeField] private GameObject inhabitantPrefab;
    [SerializeField] private GameObject demonPrefab;
    [SerializeField] private BloodlustSettings bloodLust;
    private PhotonView photonView;

    void Start()
    {
        photonView = transform.GetChild(0).GetComponent<PhotonView>();
        bloodLust = GetComponent<BloodlustSettings>();
    }

    void Update()
    {
        //if (photonView == null) transform.GetChild(0).GetComponent<PhotonView>();
        if (!photonView.IsMine) return;
        //if(bloodLust == null) bloodLust = GetComponent<BloodlustSettings>();

        if (bloodLust._demonBloodlust >= 60 && Input.GetKeyDown(KeyCode.F) && !bloodLust.isAimodipsis)
        {
            PhotonNetwork.Instantiate(demonPrefab.name, transform.GetChild(0).position, Quaternion.identity).transform.parent = transform;
            bloodLust.isAimodipsis = true;
        }
        else if (bloodLust.isAimodipsis && bloodLust._demonBloodlust <= 0)
        {
            PhotonNetwork.Instantiate(inhabitantPrefab.name, transform.GetChild(0).position, Quaternion.identity).transform.parent = transform;
            bloodLust.isAimodipsis = false;  
        }
        else return;
        PhotonNetwork.Destroy(transform.GetChild(0).gameObject);
        photonView = transform.GetChild(0).GetComponent<PhotonView>();
        bloodLust = GetComponent<BloodlustSettings>();
    }
}
