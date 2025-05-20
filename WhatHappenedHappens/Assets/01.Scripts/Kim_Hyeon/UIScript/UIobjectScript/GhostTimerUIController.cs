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
        public float timer { get; set; }

    private void Start()
    {
        _paradoxManager = FindObjectOfType<ParadoxManager>();
        if (_paradoxManager == null)
            Debug.LogError("GhostTimerUIController: ParadoxManager�� ã�� �� �����ϴ�!");

        if (ghostTimerSprites == null || ghostTimerSprites.Count == 0)
            Debug.LogWarning("ghostTimer UI Image ����Ʈ�� ����ֽ��ϴ�. �ν����Ϳ��� �Ҵ����ּ���.");
        if (timerImage == null)
            Debug.LogError("timerImage�� �Ҵ���� �ʾҽ��ϴ�.");

        timerImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 1) ��ȭ ���̰� ���� �ð��� ������ �� UI �Ѱ� ��������Ʈ ����
        if (_paradoxManager.recordingTimeRemaining > 0f)
        {

            UpdateSpriteForTime(timer);
        }

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
