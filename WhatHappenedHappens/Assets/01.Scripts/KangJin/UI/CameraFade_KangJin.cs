using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CameraFade_KangJin : MonoBehaviour
{
    public static CameraFade_KangJin instance; 
   
    [Header("PlayerPrefab")]
    [SerializeField] private GameObject PlayerPrefab; // 중심이 될 플레이어 프리팹 
   
    [Header("Fade Settings")]
    [SerializeField, Range(0.1f, 5f)] private float fadeTime = 1f;
    public Image FadeImage;                           // 페이드에 사용할 이미지 
    public bool isClear;// 스테이지 클리어 여부 확인 
    public TrueFalse truefalse;

    private void Awake()
    {
        if (PlayerPrefab == null)
            PlayerPrefab = GameObject.FindWithTag("Player");
         
        FadeImage.gameObject.SetActive(true);
        isClear = false;
        SequenceBegin();
        //Debug.Log($"임시로 false 처리 : isClear : {isClear}");
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f, fadeTime));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0f, 1f, fadeTime));
    }

    private void SequenceBegin()
    {
        FadeImage.color = new Color(0, 0, 0, 1);
        
        if (isClear) //false 일때, 켜져야 함
        {
            //fadeout
            FadeOut();
        }
        else if (isClear == false)
        {
          FadeIn();
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = FadeImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            /*Debug.Log($"현재 페이드 시간 : {elapsed}");*/
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            FadeImage.color = color;
            yield return null;
        }
        color.a = endAlpha;
        FadeImage.color = color;
    }
    private void Update()
    {
        if (isClear != truefalse.IsTrue) // 값이 달라졌을 때, 뭐하냐.....
        {
            isClear = truefalse.IsTrue; // 값 대입
            SequenceBegin(); // 페이드 인 또는 페이드 아웃 적용
        }
            Debug.Log(isClear);
    }
    private void FixedUpdate()
    {
     
    }
    public void StageClear()
    {
        isClear = true;
    }

}
