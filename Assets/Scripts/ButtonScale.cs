using UnityEngine;

public class ButtonScale : MonoBehaviour
{
    [SerializeField] private GameObject _play;
    [SerializeField] private AudioSource _playSource;

    public void BigButton()
    {
        _play.transform.localScale = new Vector3(3, 3, 3);
        _playSource.Play();
    }
    public void SmallButton()
    {
        _play.transform.localScale = new Vector3(1,1,1);
        _playSource.Pause();
    }
}
