using Photon.Pun;
using System;
using UnityEngine;

public class SpectatorCamera : MonoBehaviour
{ 
    [SerializeField] private Vector3 distance;
    [SerializeField] private GameObject spectatorPrefab;
    private GameObject parentPlayerObject;
    private Transform currentSpectatingPlayer;
    [SerializeField] private int currentPlayerIndex;
    private PhotonView photonView;
    private int sensitivityMouse;
    private float yRotation;
    private const int COEFFICIENT = 3;
    public Action<string> onChangedSpectatingPlayer;
    private void Start()
    {
        //parentPlayerObject = GetPlayerParent(gameObject);
        currentPlayerIndex = 0;
        photonView = GetComponent<PhotonView>();
        sensitivityMouse = PlayerPrefs.GetInt("Sensitivity") * COEFFICIENT;
        ChangeSpectatingPlayer(1);
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<Camera>());
            Destroy(GetComponent<AudioListener>());
            Destroy(gameObject);
        }
    }
    private GameObject GetPlayerParent(GameObject lookingPlayerFor)
    {
        CurrentPlayer[] players = FindObjectsOfType<CurrentPlayer>();
        foreach (CurrentPlayer player in players)
        {
            if (player.CurrentPlayerModel == lookingPlayerFor)
            {
                return player.gameObject;
            }
        }
        return null; 
    }
    private void Update()
    {
        if (!photonView.IsMine) return;
        if (currentSpectatingPlayer == null) ChangeSpectatingPlayer(0);
        //transform.position = currentSpectatingPlayer.position + distance;
        SpectateCamera();
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeSpectatingPlayer(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeSpectatingPlayer(1);
        }
    }

    private void ChangeSpectatingPlayer(int indexStep)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        currentPlayerIndex += indexStep;
        if (currentPlayerIndex == -1)
        {
            currentPlayerIndex = players.Length - 1;
        }
        else if(currentPlayerIndex == players.Length)
        {
            currentPlayerIndex = 0;
        }
        currentSpectatingPlayer = players[currentPlayerIndex].transform;
        transform.position = currentSpectatingPlayer.position + distance;
        transform.parent = currentSpectatingPlayer.transform;
        transform.LookAt(currentSpectatingPlayer.position + new Vector3(0, 1, 0));

        GameObject playerParent = GetPlayerParent(players[currentPlayerIndex]);
        string nickOfSpectatingPlayer = playerParent.GetComponent<PlayerNickName>().nickName;
        onChangedSpectatingPlayer?.Invoke(nickOfSpectatingPlayer);
    }

    private void SpectateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        yRotation += mouseX;
        
        transform.RotateAround(currentSpectatingPlayer.position, new Vector3(0, 1, 0), mouseX * 35  * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
