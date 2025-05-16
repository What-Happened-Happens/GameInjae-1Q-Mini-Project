using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTimer : MonoBehaviour
{
    [Header("GhostTimerImages")] // ��Ʈ Ÿ�̸� �̹��� ����Ʈ 
    public List<Image> ghostTimers = new List<Image>();
    ParadoxManager _paradoxManager;

    private void Start()
    {
        if (_paradoxManager == null) _paradoxManager = gameObject.AddComponent<ParadoxManager>();
        if (ghostTimers == null || ghostTimers.Count == 0)
            Debug.LogWarning("ghostTimer UI Image ����Ʈ�� ����ֽ��ϴ�. �ν����Ϳ��� �Ҵ����ּ���.");
         
        foreach (var timerImg in ghostTimers)
        {
            if (timerImg != null)
                timerImg.gameObject.SetActive(true); 
        }
       

    }

    private void Update()
    {
        float timer = _paradoxManager.recordingTimeRemaining; // ��ȭ �� �����ִ� �ð�. 

    }
    // �����ִ� �ð��� �޾Ƽ�, �����ִ� �ð� ���� Ÿ�̸� ��������Ʈ�� ����. 
    // �����ִ� �ð��� ���� ���ʹٴ� ���� �Ǿ�� �ϱ� ������, Update���� ȣ�� �ʿ�
    public void GhostTimerSpriteChanged(float timeRemaining)
    {

    }
}
