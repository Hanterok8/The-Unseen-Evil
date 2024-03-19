using UnityEngine;

public class LabirintScrimer : MonoBehaviour
{
    [SerializeField] private Animator _leftHand;
    [SerializeField] private Animator _rightHand;
    [SerializeField] private GameObject _leftHandImage;
    [SerializeField] private GameObject _rightHandImage;

    private void OnTriggerEnter(Collider other)
    {
        _leftHandImage.SetActive(true);
        _rightHandImage.SetActive(true);
    }
}
