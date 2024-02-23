
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    void Start()
    {
        if (!Application.isEditor)
             Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }

    
}
