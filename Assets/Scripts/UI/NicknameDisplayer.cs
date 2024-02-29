using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class NicknameDisplayer : MonoBehaviour
{
    private Transform camera;
    private PhotonView photonView;
    
    void Start()
    {
        camera = Camera.main.transform;
        photonView = transform.parent.GetComponent<PhotonView>();
        transform.GetChild(0).GetComponent<TMP_Text>().text = photonView.Owner.NickName;
    }
    
    private void Update()
    {
        if (photonView.IsMine)
        {
            Destroy(gameObject);
            return;
        }
        transform.LookAt(camera);
    }
}
