using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTimer : MonoBehaviour
{

    private ParadoxManager _paradoxManager;

    [Header("GhostTimerImages")] // ��Ʈ Ÿ�̸� �̹��� ����Ʈ 
    public List<Sprite> ghostTimers = new List<Sprite>(); // ������ ��������Ʈ ����Ʈ 
    [SerializeField] private Image timerImage;            // ��Ʈ Ÿ�̸� �̹���    

    private void Start()
    {
        if (_paradoxManager == null) _paradoxManager = gameObject.AddComponent<ParadoxManager>();
        if (ghostTimers == null || ghostTimers.Count == 0)
            Debug.LogWarning("ghostTimer UI Image ����Ʈ�� ����ֽ��ϴ�. �ν����Ϳ��� �Ҵ����ּ���.");


    }

    private void Update()
    {
        float timer = _paradoxManager.recordingTimeRemaining; // ��ȭ �� �����ִ� �ð�. 

    }
    // �����ִ� �ð��� �޾Ƽ�, �����ִ� �ð� ���� Ÿ�̸� ��������Ʈ�� ����. 
    // �����ִ� �ð��� ���� ���ʹٴ� ���� �Ǿ�� �ϱ� ������, Update���� ȣ�� �ʿ�
    public void GhostTimerSpriteChanged(float timeRemaining )
    {
        if (timeRemaining < 0 || timeRemaining < 5f || ghostTimers.Count == 0) return;
        Debug.LogWarning($"�����ִ� �ð��� �ʰ��߽��ϴ�. timeRemaining : {timeRemaining}");



    }
}
