using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTimerUIController : UIHelper
{
    // private ParadoxManager _paradoxManager;

    [Header("GhostTimerImages")] // 고스트 타이머 이미지 리스트 
    public List<Sprite> ghostTimerSprites = new List<Sprite>(); // 변경할 스프라이트 리스트 
    [SerializeField] private Image timerImage;            // 고스트 타이머 이미지    

    private float tickAcc = 0f;
    public float timer { get; set; }

    private void Start()
    {
        // if (_paradoxManager == null) _paradoxManager = gameObject.AddComponent<ParadoxManager>();
        if (ghostTimerSprites == null || ghostTimerSprites.Count == 0)
            Debug.LogWarning("ghostTimer UI Image 리스트가 비어있습니다. 인스펙터에서 할당해주세요.");

        //timer = _paradoxManager.recordingTimeRemaining; // 녹화 중 남아있는 시간. 
        timer = 5f; // 녹화 중 남아있는 시간. 
    }

    private void Update()
    {
        if (timer <= 0f) return;

        tickAcc += Time.deltaTime;
        if (tickAcc >= 1f)
        {
            timer -= 1f;
            tickAcc -= 1f;
        }

        UpdateSpriteForTime(timer);
    }

    // 남아있는 시간을 받아서, 남아있는 시간 동안 타이머 스프라이트가 변경. 
    // 남아있는 시간에 따라서 매초바다 변경 되어야 하기 때문에, Update에서 호출 필요
    public void UpdateSpriteForTime(float timeRemaining)
    {
        if (timeRemaining <= 0f) return;
        Debug.Log($"ghostTimer : 스프라이트 개수 : {ghostTimerSprites.Count}");
        Debug.Log($"ghostTimer : 현재 타이머 시간  : {timeRemaining}");

        int idx = Mathf.Clamp(Mathf.CeilToInt(timeRemaining) - 1, 0, ghostTimerSprites.Count - 1);
        Debug.Log($"현재 스프라이트 : {ghostTimerSprites[idx]}");
        SetImageSprite(timerImage, ghostTimerSprites[idx]);

    }
}
