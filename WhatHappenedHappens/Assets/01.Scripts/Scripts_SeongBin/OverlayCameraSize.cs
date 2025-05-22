using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Overlays;
#endif
using UnityEngine;

public class OverlayCameraSize : MonoBehaviour
{
    public PixelPerfectZoomCinemachine virCamerasize;
    Camera CameraTmp;
    // Start is called before the first frame update
    void Start()
    {
        CameraTmp = GetComponent<Camera>();
        CameraTmp.orthographicSize = virCamerasize.defaultCameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        CameraTmp.orthographicSize = virCamerasize.currentZoom;
    }
}
