using UnityEngine;
using Photon.Pun;

public class IsAimodipsis : MonoBehaviour
{
    public bool isAimodipsis;
    private void Start()
    {
        isAimodipsis = false;
    }
    public void SetAimodipsisMode(bool setInto)
    {
        isAimodipsis = setInto;
    }
}
