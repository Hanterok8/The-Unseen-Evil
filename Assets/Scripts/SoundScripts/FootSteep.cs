using System.Collections.Generic;
using UnityEngine;

public class FootSteep : MonoBehaviour
{
    [SerializeField] public AudioClip[] _footSound;
    [SerializeField] public AudioSource _footSourse;
    private int _randomSound;
    void Start()
    {
        _footSound = Resources.LoadAll<AudioClip>("FootSound");
    }
    private void Play()
    {
        _randomSound = Random.Range(0, 3);
        _footSourse.PlayOneShot(_footSound[_randomSound]);
    }
}
