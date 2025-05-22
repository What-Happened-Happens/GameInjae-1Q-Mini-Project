using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTimerUIController : UIHelper
{
    [SerializeField] private ParadoxManager _paradoxManager;

    [Header("GhostTimerImages")] // 고스트 타이머 이미지 리스트 
    public List<Sprite> ghostTimerSprites = new List<Sprite>(); // 변경할 스프라이트 리스트 
    [SerializeField] private Image timerImage;            // 고스트 타이머 이미지    

    private float tickAcc = 0f;
    public float timer { get; set; }

    float totalDuration;
    int spriteCount;

    private void Start()
    {
        totalDuration = _paradoxManager.recordingDuration;
        spriteCount = ghostTimerSprites.Count;

        timerImage.gameObject.SetActive(false);

        _paradoxManager = FindObjectOfType<ParadoxManager>();
        if (_paradoxManager == null)
        {
            Debug.LogError("GhostTimerUIController: ParadoxManager를 찾을 수 없습니다!");
            enabled = false;    // 이 스크립트 자체를 비활성화해서 Update 진입 자체를 차단
            return;
        }
        if (ghostTimerSprites == null || ghostTimerSprites.Count == 0)
            Debug.LogWarning("ghostTimer UI Image 리스트가 비어있습니다. 인스펙터에서 할당해주세요.");
        
        
    }

    private void Update()
    {
        timer = _paradoxManager.recordingTimeRemaining; // 녹화 중 남아있는 시간

        if (_paradoxManager.isRecording == true )
        {
            // Debug.Log($"녹화 시작 지점"); 
            timerImage.gameObject.SetActive(true);

            UpdateSpriteForTime(timer);
        }
        else if (timerImage.gameObject.activeSelf && _paradoxManager.isRecording == false )
        {
            // Debug.Log($"녹화 종료 지점");
            timerImage.gameObject.SetActive(false);
        }


    }

    public void UpdateSpriteForTime(float timeRemaining)
    {
        if (spriteCount == 0 || totalDuration <= 0f) return;

        // 얼마나 진행되었는지 비율 계산
        float progress = Mathf.Clamp01(1f - (timeRemaining / totalDuration)); // 0 → 1로 진행

        // 현재 보여줄 스프라이트 인덱스 계산
        int index = Mathf.FloorToInt(progress * spriteCount);
        index = Mathf.Clamp(index, 0, spriteCount - 1);

        SetImageSprite(timerImage, ghostTimerSprites[index]);
    }
}

