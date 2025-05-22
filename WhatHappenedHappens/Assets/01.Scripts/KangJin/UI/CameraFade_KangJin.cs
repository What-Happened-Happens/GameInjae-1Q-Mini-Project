using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CameraFade_KangJin : MonoBehaviour
{
    public static CameraFade_KangJin instance; 
   
    [Header("PlayerPrefab")]
    [SerializeField] private GameObject PlayerPrefab; // �߽��� �� �÷��̾� ������ 
   
    [Header("Fade Settings")]
    [SerializeField, Range(0.1f, 5f)] private float fadeTime = 1f;
    public Image FadeImage;                           // ���̵忡 ����� �̹��� 
    public bool isClear;// �������� Ŭ���� ���� Ȯ�� 
    public TrueFalse truefalse;

    private void Awake()
    {
        if (PlayerPrefab == null)
            PlayerPrefab = GameObject.FindWithTag("Player");
         
        FadeImage.gameObject.SetActive(true);
        isClear = false;
        SequenceBegin();
        //Debug.Log($"�ӽ÷� false ó�� : isClear : {isClear}");
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
        
        if (isClear) //false �϶�, ������ ��
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
            /*Debug.Log($"���� ���̵� �ð� : {elapsed}");*/
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            FadeImage.color = color;
            yield return null;
        }
        color.a = endAlpha;
        FadeImage.color = color;
    }
    private void Update()
    {
        if (isClear != truefalse.IsTrue) // ���� �޶����� ��, ���ϳ�.....
        {
            isClear = truefalse.IsTrue; // �� ����
            SequenceBegin(); // ���̵� �� �Ǵ� ���̵� �ƿ� ����
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
