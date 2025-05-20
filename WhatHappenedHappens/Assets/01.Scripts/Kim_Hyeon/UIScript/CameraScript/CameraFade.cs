using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CameraFade : MonoBehaviour
{
    [Header("PlayerPrefab")]
    [SerializeField] private GameObject PlayerPrefab; // 중심이 될 플레이어 프리팹 
    [SerializeField] private Camera CameraPrefab;     // 플레이어를 따라 다닐 카메라 
    public Image FadeImage;                           // 페이드에 사용할 이미지 
    public bool isClear { get; set; }  // 스테이지 클리어 여부 확인 

    [Header("Fade Settings")]
    [SerializeField, Range(0.1f, 5f)] private float fadeTime = 1f;

    [Header("Zoom Settings")]
    [SerializeField, Range(0.1f, 20f)] private float zoomInSize = 3f;
    [SerializeField, Range(0.1f, 20f)] private float zoomOutSize = 5f;
    [SerializeField, Range(0.1f, 5f)] private float zoomTime = 2f;

    private void Awake()
    {
        if (CameraPrefab == null) CameraPrefab = Camera.main;
        if (PlayerPrefab == null)
            PlayerPrefab = GameObject.FindWithTag("Player");

        FadeImage.gameObject.SetActive(false);

        isClear = true;
        Debug.Log($"임시로 false 처리 : isClear : {isClear}");
        StartCoroutine(SequenceBegin());
    }

    private IEnumerator SequenceBegin()
    {
        FadeImage.color = new Color(0, 0, 0, 1);
        CameraPrefab.orthographicSize = zoomOutSize;
        FadeImage.gameObject.SetActive(true);
        if (isClear)
        {
            yield return StartCoroutine(Fade(1f, 0f, fadeTime));
            yield return StartCoroutine(ZoomCamera(zoomInSize, zoomOutSize, zoomTime));
            Debug.Log($"Fade 값 : {Fade(1f, 0f, fadeTime)}");
            Debug.Log($"줌 값 : {ZoomCamera(zoomOutSize, zoomInSize, zoomTime)}");
        }
        else if (isClear == false)
        {
            yield return StartCoroutine(Fade(0f, 1f, fadeTime));
            Debug.Log($"Fade 값 : {Fade(0f, 1f, fadeTime)}");
            yield return StartCoroutine(ZoomCamera(zoomOutSize, zoomInSize, zoomTime));
            Debug.Log($"줌 값 : {ZoomCamera(zoomOutSize, zoomInSize, zoomTime)}");
        }
    }
    private IEnumerator WaitForClear()
    {
        while (!isClear)
            yield return null;
    }
    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = FadeImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Debug.Log($"현재 페이드 시간 : {elapsed}");
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            FadeImage.color = color;
            yield return null;
        }
        color.a = endAlpha;
        FadeImage.color = color;
    }

    private IEnumerator ZoomCamera(float fromSize, float toSize, float duration)
    {
        float elapsed = 0f;
        float cameraZpos = CameraPrefab.transform.position.z;

        Vector3 startPos = CameraPrefab.transform.position;
        Debug.Log($"시작 위치 [x] : {startPos.x}. [y] : {startPos.y}");

        Vector3 targetPos = PlayerPrefab.transform.position;
        Debug.Log($"시작 위치 [x] : {targetPos.x}. [y] : {targetPos.y}");
        targetPos.z = cameraZpos;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            CameraPrefab.orthographicSize = Mathf.Lerp(fromSize, toSize, t);
            CameraPrefab.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        try
        {
            CameraPrefab.orthographicSize = toSize;
            Vector3 finalPos = targetPos;
            finalPos.z = cameraZpos;
            CameraPrefab.transform.position = finalPos;
        }
        catch (Exception e)
        {
            Debug.LogError($"SoundValueLoad 중 예외 발생: {e}");
            throw e;
            // Error 출력
        }


    }
    private void Update()
    {
        // 플레이어 위치 따라가기
        if (PlayerPrefab != null)
        {
            Vector3 playerpos = PlayerPrefab.transform.position;
            CameraPrefab.transform.position = new Vector3(playerpos.x, playerpos.y, CameraPrefab.transform.position.z);
        }
    }
    public void StageClear()
    {
        isClear = true;
    }

}
