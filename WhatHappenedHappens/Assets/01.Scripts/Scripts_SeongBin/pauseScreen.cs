using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    public PixelPerfectZoomCinemachine ZoomCamera;
    // Start is called before the first frame update

    void Update()
    {
        
        if (ZoomCamera.isScreenWide)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

}

