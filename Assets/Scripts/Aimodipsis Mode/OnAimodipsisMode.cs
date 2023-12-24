using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class OnAimodipsisMode : MonoBehaviour
{
    [SerializeField] private GameObject inhabitantPrefab;
    [SerializeField] private GameObject demonPrefab;
    [SerializeField] private BloodlustSettings bloodLust;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        bloodLust = GetComponent<BloodlustSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        if(bloodLust == null) bloodLust = GetComponent<BloodlustSettings>();
        if (bloodLust._demonBloodlust >= 60 && Input.GetKeyDown(KeyCode.F) && !bloodLust.isAimodipsis)
        {
            PhotonNetwork.Instantiate(demonPrefab.name, transform.GetChild(0).position, Quaternion.identity).transform.parent = transform;
            Destroy(transform.GetChild(0).gameObject);
            bloodLust.isAimodipsis = true;
            photonView = transform.GetChild(0).GetComponent<PhotonView>();
            bloodLust = transform.GetChild(0).GetComponent<BloodlustSettings>();
        }
        else if (bloodLust.isAimodipsis && bloodLust._demonBloodlust <= 0)
        {
            PhotonNetwork.Instantiate(inhabitantPrefab.name, transform.GetChild(0).position, Quaternion.identity).transform.parent = transform;
            Destroy(transform.GetChild(0).gameObject);
            bloodLust.isAimodipsis = false;
            photonView = transform.GetChild(0).GetComponent<PhotonView>();
            bloodLust = transform.GetChild(0).GetComponent<BloodlustSettings>();

        }
    }
}
