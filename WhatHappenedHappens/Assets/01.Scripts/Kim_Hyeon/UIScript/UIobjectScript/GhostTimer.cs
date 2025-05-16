using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTimer : MonoBehaviour
{

    private ParadoxManager _paradoxManager;

    [Header("GhostTimerImages")] // 고스트 타이머 이미지 리스트 
    public List<Sprite> ghostTimers = new List<Sprite>(); // 변경할 스프라이트 리스트 
    [SerializeField] private Image timerImage;            // 고스트 타이머 이미지    

    private void Start()
    {
        if (_paradoxManager == null) _paradoxManager = gameObject.AddComponent<ParadoxManager>();
        if (ghostTimers == null || ghostTimers.Count == 0)
            Debug.LogWarning("ghostTimer UI Image 리스트가 비어있습니다. 인스펙터에서 할당해주세요.");


    }

    private void Update()
    {
        float timer = _paradoxManager.recordingTimeRemaining; // 녹화 중 남아있는 시간. 

    }
    // 남아있는 시간을 받아서, 남아있는 시간 동안 타이머 스프라이트가 변경. 
    // 남아있는 시간에 따라서 매초바다 변경 되어야 하기 때문에, Update에서 호출 필요
    public void GhostTimerSpriteChanged(float timeRemaining )
    {
        if (timeRemaining < 0 || timeRemaining < 5f || ghostTimers.Count == 0) return;
        Debug.LogWarning($"남아있는 시간을 초과했습니다. timeRemaining : {timeRemaining}");



    }
}
