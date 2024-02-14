using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectateCanvas : DisablePlayerCanvas
{
    public override void DisableCanvas()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
