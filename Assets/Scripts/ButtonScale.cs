using UnityEngine;

public class ButtonScale : MonoBehaviour
{
    [SerializeField] private Animator _play;
    [SerializeField] private AudioSource _playSource;

    public bool isClicked { get; private set; }

    public void BigButton()
    {
        if (isClicked) return;
        _play.SetTrigger("MakeBig");
        _playSource.Play();
    }
    public void SmallButton()
    {
        if (isClicked) return;
        _play.SetTrigger("MakeSmall");
        _playSource.Pause();
    }

    public void MiddleButton()
    {
        _play.SetTrigger("MakeMiddle");
        isClicked = true;
    }
    
}
