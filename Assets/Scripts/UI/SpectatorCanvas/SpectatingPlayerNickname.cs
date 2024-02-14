using TMPro;
using UnityEngine;

public class SpectatingPlayerNickname : MonoBehaviour
{
    private SpectatorCamera spectatorCamera;
    private TMP_Text nickname;
    private void OnEnable()
    {
        GameObject spectator = GameObject.FindGameObjectWithTag("Spectator");
        spectatorCamera = spectator.GetComponent<SpectatorCamera>();
        nickname = GetComponent<TMP_Text>();
        spectatorCamera.onChangedSpectatingPlayer += DisplayNewNickname;
    }
    private void OnDisable()
    {
        spectatorCamera.onChangedSpectatingPlayer -= DisplayNewNickname;
    }
    private void DisplayNewNickname(string newNickname)
    {
        nickname.text = newNickname;
    }

}
