using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTimerUIController : UIHelper
{
    [SerializeField] private ParadoxManager _paradoxManager;

    [Header("GhostTimerImages")] // ��Ʈ Ÿ�̸� �̹��� ����Ʈ 
    public List<Sprite> ghostTimerSprites = new List<Sprite>(); // ������ ��������Ʈ ����Ʈ 
    [SerializeField] private Image timerImage;            // ��Ʈ Ÿ�̸� �̹���    

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
            Debug.LogError("GhostTimerUIController: ParadoxManager�� ã�� �� �����ϴ�!");
            enabled = false;    // �� ��ũ��Ʈ ��ü�� ��Ȱ��ȭ�ؼ� Update ���� ��ü�� ����
            return;
        }
        if (ghostTimerSprites == null || ghostTimerSprites.Count == 0)
            Debug.LogWarning("ghostTimer UI Image ����Ʈ�� ����ֽ��ϴ�. �ν����Ϳ��� �Ҵ����ּ���.");
        
        
    }

    private void Update()
    {
        timer = _paradoxManager.recordingTimeRemaining; // ��ȭ �� �����ִ� �ð�

        if (_paradoxManager.isRecording == true )
        {
            // Debug.Log($"��ȭ ���� ����"); 
            timerImage.gameObject.SetActive(true);

            UpdateSpriteForTime(timer);
        }
        else if (timerImage.gameObject.activeSelf && _paradoxManager.isRecording == false )
        {
            // Debug.Log($"��ȭ ���� ����");
            timerImage.gameObject.SetActive(false);
        }


    }

    public void UpdateSpriteForTime(float timeRemaining)
    {
        if (spriteCount == 0 || totalDuration <= 0f) return;

        // �󸶳� ����Ǿ����� ���� ���
        float progress = Mathf.Clamp01(1f - (timeRemaining / totalDuration)); // 0 �� 1�� ����

        // ���� ������ ��������Ʈ �ε��� ���
        int index = Mathf.FloorToInt(progress * spriteCount);
        index = Mathf.Clamp(index, 0, spriteCount - 1);

        SetImageSprite(timerImage, ghostTimerSprites[index]);
    }
}

