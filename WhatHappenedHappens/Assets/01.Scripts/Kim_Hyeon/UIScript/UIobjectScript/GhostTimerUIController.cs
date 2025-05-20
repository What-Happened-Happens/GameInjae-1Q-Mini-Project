using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTimerUIController : UIHelper
{
     private ParadoxManager _paradoxManager;

    [Header("GhostTimerImages")] // ��Ʈ Ÿ�̸� �̹��� ����Ʈ 
    public List<Sprite> ghostTimerSprites = new List<Sprite>(); // ������ ��������Ʈ ����Ʈ 
    [SerializeField] private Image timerImage;            // ��Ʈ Ÿ�̸� �̹���    

    private float tickAcc = 0f;
    public float timer { get; set; }

    private void Start()
    {
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
        timer = _paradoxManager.recordingTimeRemaining; // ��ȭ �� �����ִ� �ð�. 
        Debug.Log("timer = " + timer);

        // Debug.Log("�� GhostTimerUIController.Update ȣ��!");

        if (_paradoxManager.isRecording == true && timer <= 5f)
        {
            Debug.Log($"��ȭ ���� ����GGGGGGGGGGGG"); 
            timerImage.gameObject.SetActive(true);

            UpdateSpriteForTime(timer);
        }
        else if (timerImage.gameObject.activeSelf && _paradoxManager.isRecording == false
            && timer >= 0f)
        {
            Debug.Log($"��ȭ ���� ����");
            timerImage.gameObject.SetActive(false);
        }


    }

    // �����ִ� �ð��� �޾Ƽ�, �����ִ� �ð� ���� Ÿ�̸� ��������Ʈ�� ����. 
    // �����ִ� �ð��� ���� ���ʹٴ� ���� �Ǿ�� �ϱ� ������, Update���� ȣ�� �ʿ�
    public void UpdateSpriteForTime(float timeRemaining)
    {
        Debug.Log($"ghostTimer : ��������Ʈ ���� : {ghostTimerSprites.Count}");
        Debug.Log($"ghostTimer : ���� Ÿ�̸� �ð�  : {timeRemaining}");

        int idx = Mathf.Clamp(Mathf.CeilToInt(timeRemaining) - 1, 0, ghostTimerSprites.Count - 1);
        Debug.Log($"���� ��������Ʈ : {ghostTimerSprites[idx]}");
        SetImageSprite(timerImage, ghostTimerSprites[idx]);

    }
}

