using UnityEngine;
using Photon.Pun;
using System;

public class IsAimodipsis : MonoBehaviour
{
    public bool isAimodipsis { get; private set; }
    public Action<bool> onAimodipsisFlagChanged;

    private void Start()
    {
        isAimodipsis = false;
    }
    public void SetAimodipsisMode(bool setInto)
    {
        isAimodipsis = setInto;
        onAimodipsisFlagChanged?.Invoke(setInto);
    }
}
