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
        seeSettings.GetComponent<Animator>().SetTrigger("Close");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
