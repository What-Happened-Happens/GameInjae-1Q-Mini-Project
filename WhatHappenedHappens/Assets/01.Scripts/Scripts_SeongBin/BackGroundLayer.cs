using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackGroundLayer : MonoBehaviour
{


    [Header("리핏 배경 쿼드 렌더러들")]
    public List<Renderer> backgroundRenderers;

    [Header("카메라")]
    public Transform cam;

    [Header("패럴럭스 강도 (멀수록 덜 움직임)")]
    public float parallaxStrength = 0.05f;

    [Header("메인 카메라 scripts")]
    public PixelPerfectZoomCinemachine virtualZoom;
    public pauseScreen pulse;



    float nowSize;
    float mulSize;
    private List<Material> materials;
    private List<float> zDepths;
    private List<Vector3> initialScales;
    private Vector3 lastCamPos;
    private Vector3 initialScale;

    void Awake()
    {
        if (cam == null)
            cam = Camera.main.transform;
    }

    void Start()
    {
        initialScales = new List<Vector3>();
        materials = new List<Material>();
        zDepths = new List<float>();
        initialScale = new Vector3(18.5f, 10.5f, 1); // 초기 스케일 저장
        foreach (var renderer in backgroundRenderers)
        {
            Material matInstance = new Material(renderer.sharedMaterial); // 재질 복제 (인스턴스화)
            renderer.material = matInstance;
            materials.Add(matInstance);
            zDepths.Add(renderer.transform.position.z);
            initialScales.Add(renderer.transform.localScale);
        }

        if (cam != null)
            lastCamPos = cam.position;
        Debug.Log("asd"+initialScale);

    }

    void FixedUpdate()
    {
        if (!pulse.isScreenWide)
        {
            CameraScroll();
        }

    }

    private void Update()
    {
        if (pulse.isScreenWide)
        {
            CameraScroll();
        }
    }

    void CameraScroll(){
        if (cam == null) return;

        Vector3 camDelta = cam.position - lastCamPos;
        ScaleCal();

        for (int i = 0; i < backgroundRenderers.Count; i++)
        {
            float depthFactor = 1f - Mathf.InverseLerp(GetMinZ(), GetMaxZ(), zDepths[i]);
            float scrollScale = depthFactor * parallaxStrength * 0.02f;

            // 텍스처 스크롤 (Repeat 효과)
            Vector2 offset = materials[i].mainTextureOffset;
            offset += new Vector2(camDelta.x * scrollScale, camDelta.y * scrollScale);
            materials[i].mainTextureOffset = offset;
            materials[i].mainTextureScale = new Vector2(1, 1) * mulSize;
            // 쿼드는 항상 카메라 위치에 고정
            backgroundRenderers[i].transform.position = new Vector3(cam.position.x, cam.position.y, zDepths[i]);

            // 줌에 따른 크기 조정
            backgroundRenderers[i].transform.localScale = initialScales[i] * mulSize;
        }

        lastCamPos = cam.position;
    }

    float GetMinZ()
    {
        float min = float.MaxValue;
        foreach (float z in zDepths)
            if (z < min) min = z;
        return min;
    }

    float GetMaxZ()
    {
        float max = float.MinValue;
        foreach (float z in zDepths)
            if (z > max) max = z;
        return max;
    }

    void ScaleCal()
    {
        nowSize = virtualZoom.currentZoom;
        mulSize = nowSize / 5f;
    }
}
 