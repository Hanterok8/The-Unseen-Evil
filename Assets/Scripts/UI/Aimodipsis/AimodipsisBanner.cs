using System;
using UnityEngine;

public class AimodipsisBanner : MonoBehaviour
{
    private IsAimodipsis isAimodipsis;
    private GameObject banner; 

    private void Start()
    {
        banner = transform.GetChild(0).gameObject;
        banner.SetActive(false);
        isAimodipsis = FindObjectOfType<IsAimodipsis>();
        isAimodipsis.onAimodipsisFlagChanged += ActivateBanner;
    }

    private void OnDisable()
    {
        isAimodipsis.onAimodipsisFlagChanged -= ActivateBanner;
    }

    private void ActivateBanner(bool isActivating)
    {
        banner.SetActive(isActivating);
    }
}
