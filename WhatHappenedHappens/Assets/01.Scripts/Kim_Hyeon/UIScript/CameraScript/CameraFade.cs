using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CameraFade : MonoBehaviour
{
    public static CameraFade instance; 
    private TrueFalse tr;
    private Button_OnOffPlatform onoff; 

    [Header("PlayerPrefab")]
    [SerializeField] private GameObject PlayerPrefab; // �߽��� �� �÷��̾� ������ 
    [SerializeField] private Camera CameraPrefab;     // �÷��̾ ���� �ٴ� ī�޶� 
    public Image FadeImage;                           // ���̵忡 ����� �̹��� 
    public bool isClear;                              // �������� Ŭ���� ���� Ȯ�� 

    [Header("Fade Settings")]
    [SerializeField, Range(0.1f, 5f)] private float fadeTime = 1f;

    private void Awake()
    {
        if (CameraPrefab == null) CameraPrefab = Camera.main;
        if (PlayerPrefab == null)
            PlayerPrefab = GameObject.FindWithTag("Player");
         tr = gameObject.AddComponent<TrueFalse>(); 
        onoff = FindObjectOfType<Button_OnOffPlatform>(); 

        FadeImage.gameObject.SetActive(true);

        //Debug.Log($"�ӽ÷� false ó�� : isClear : {isClear}");
        SequenceBegin();
        
    }

    private void Update()
    {
        isClear = onoff.platformOn;
    }
    private void SequenceBegin()
    {
        FadeImage.color = new Color(0, 0, 0, 1);
      
        if (isClear)
        {
            //FadeOut
            StartCoroutine(Fade(0f, 1f, fadeTime));
            // Debug.Log($"Fade �� : {Fade(0f, 1f, fadeTime)}");
            // Debug.Log($"�� �� : {ZoomCamera(zoomOutSize, zoomInSize, zoomTime)}");
        }
        else if (isClear == false)
        {
           //FadeIn
            StartCoroutine(Fade(1f, 0f, 1f));
            // Debug.Log($"Fade �� : {Fade(1f, 0f, fadeTime)}");
            // Debug.Log($"�� �� : {ZoomCamera(zoomOutSize, zoomInSize, zoomTime)}");
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
            //  Debug.Log($"���� ���̵� �ð� : {elapsed}");
            float t = Mathf.Clamp01(elapsed / duration);
            color.a = Mathf.Lerp(startAlpha, endAlpha,t);
            FadeImage.color = color;
            yield return null;
        }
        color.a = endAlpha;
        FadeImage.color = color;
    }

    
    public void StageClear()
    {
        isClear = true;
    }

}
