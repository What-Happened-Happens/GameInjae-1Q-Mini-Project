using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTimerUIController : UIHelper
{
    // private ParadoxManager _paradoxManager;

    [Header("GhostTimerImages")] // ��Ʈ Ÿ�̸� �̹��� ����Ʈ 
    public List<Sprite> ghostTimerSprites = new List<Sprite>(); // ������ ��������Ʈ ����Ʈ 
    [SerializeField] private Image timerImage;            // ��Ʈ Ÿ�̸� �̹���    

    private float tickAcc = 0f;
    public float timer { get; set; }

    private void Start()
    {
        // if (_paradoxManager == null) _paradoxManager = gameObject.AddComponent<ParadoxManager>();
        if (ghostTimerSprites == null || ghostTimerSprites.Count == 0)
            Debug.LogWarning("ghostTimer UI Image ����Ʈ�� ����ֽ��ϴ�. �ν����Ϳ��� �Ҵ����ּ���.");

        //timer = _paradoxManager.recordingTimeRemaining; // ��ȭ �� �����ִ� �ð�. 
        timer = 5f; // ��ȭ �� �����ִ� �ð�. 
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

    // �����ִ� �ð��� �޾Ƽ�, �����ִ� �ð� ���� Ÿ�̸� ��������Ʈ�� ����. 
    // �����ִ� �ð��� ���� ���ʹٴ� ���� �Ǿ�� �ϱ� ������, Update���� ȣ�� �ʿ�
    public void UpdateSpriteForTime(float timeRemaining)
    {
        if (timeRemaining <= 0f) return;
        Debug.Log($"ghostTimer : ��������Ʈ ���� : {ghostTimerSprites.Count}");
        Debug.Log($"ghostTimer : ���� Ÿ�̸� �ð�  : {timeRemaining}");

        int idx = Mathf.Clamp(Mathf.CeilToInt(timeRemaining) - 1, 0, ghostTimerSprites.Count - 1);
        Debug.Log($"���� ��������Ʈ : {ghostTimerSprites[idx]}");
        SetImageSprite(timerImage, ghostTimerSprites[idx]);

    }
}
