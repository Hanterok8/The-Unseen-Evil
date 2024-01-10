using UnityEngine;

public class FootSteep : MonoBehaviour
{
    [SerializeField] public AudioClip _footSound;
    [SerializeField] public AudioSource _footSourse;
    private void Foot()
    {
        _footSourse.PlayOneShot(_footSound);
    }
}
