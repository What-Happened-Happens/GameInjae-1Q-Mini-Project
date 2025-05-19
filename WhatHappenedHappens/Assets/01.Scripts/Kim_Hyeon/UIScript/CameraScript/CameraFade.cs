using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CameraFade : MonoBehaviour
{
    [Header("PlayerPrefab")]
    [SerializeField] private GameObject PlayerPrefab; // �߽��� �� �÷��̾� ������ 
    [SerializeField] private Camera CameraPrefab;     // �÷��̾ ���� �ٴ� ī�޶� 
    public Image FadeImage;                           // ���̵忡 ����� �̹��� 
    public bool isClear { get; set; }  // �������� Ŭ���� ���� Ȯ�� 

    [Header("Fade Settings")]
    [SerializeField, Range(0.1f, 5f)] private float fadeTime = 1f;

    [Header("Zoom Settings")]
    [SerializeField, Range(0.1f, 20f)] private float zoomInSize = 3f;
    [SerializeField, Range(0.1f, 20f)] private float zoomOutSize = 5f;
    [SerializeField, Range(0.1f, 5f)] private float zoomTime = 1f;     

    private void Awake()
    {
        if (CameraPrefab == null) CameraPrefab = Camera.main;
        if (PlayerPrefab == null)
            PlayerPrefab = GameObject.FindWithTag("Player");

        isClear = false;
        Debug.Log($"�ӽ÷� false ó�� : isClear : {isClear}"); 
        StartCoroutine(SequenceBegin()); 
    }

    private IEnumerator SequenceBegin()
    {
        FadeImage.color = new Color(0, 0, 0, 1);
        CameraPrefab.orthographicSize = zoomOutSize;       

        if (isClear)
        {
            yield return StartCoroutine(Fade(1f, 0f, fadeTime));
            yield return StartCoroutine(ZoomCamera(zoomOutSize, zoomInSize, zoomTime));
        }
        else if (!isClear)
        {
            yield return StartCoroutine(Fade(0f, 1f, fadeTime));
            yield return StartCoroutine(ZoomCamera(zoomInSize, zoomOutSize, zoomTime));
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

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Debug.Log($"���� ���̵� �ð� : {elapsed}");
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
        float FadeTime = Mathf.Clamp01(elapsed / duration);
        Vector3 startPos = CameraPrefab.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (isClear)
            {
                CameraPrefab.orthographicSize = Mathf.Lerp(fromSize, toSize, FadeTime);
                Vector3 playerPos = PlayerPrefab.transform.position;
                playerPos.z = cameraZpos;
                CameraPrefab.transform.position = Vector3.Lerp(startPos, playerPos, FadeTime);

                yield return null;
            }
            else if (isClear == false)
            {
                CameraPrefab.orthographicSize = Mathf.Lerp(toSize, fromSize, FadeTime);
                Vector3 playerPos = PlayerPrefab.transform.position;
                playerPos.z = cameraZpos;
                CameraPrefab.transform.position = Vector3.Lerp(startPos, playerPos, FadeTime);

                yield return null;
            }
           

        }

        try
        {
            CameraPrefab.orthographicSize = toSize;
            Vector3 finalPos = PlayerPrefab.transform.position;
            finalPos.z = cameraZpos;
            CameraPrefab.transform.position = finalPos;
        }
        catch (Exception e)
        {
            Debug.LogError($"SoundValueLoad �� ���� �߻�: {e}");
            throw e;
            // Error ���
        }

     
    }
    private void LateUpdate()
    {
        // �÷��̾� ��ġ ���󰡱�
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
