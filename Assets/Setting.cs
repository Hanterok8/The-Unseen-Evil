using UnityEngine;

public class Setting : MonoBehaviour
{
    [SerializeField] private GameObject seeSettings;
    public void OnSettings()
    {
        seeSettings.SetActive(true);
    }
    public void OffSettings()
    {
        seeSettings.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
