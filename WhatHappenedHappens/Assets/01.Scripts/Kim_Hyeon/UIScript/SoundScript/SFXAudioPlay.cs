using UnityEngine;

public class SFXAudioPlay : SFXAudioManager
{
    [Header("Short or Long Threshold")]
    [SerializeField] private float shortThreshold = 1f;

    [Header("재생 기준 시간")]
    [SerializeField] private float clickSoundDepth = 1.5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ClickEventAudioPlay();

    }

    public void ClickEventAudioPlay()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = clickSoundDepth;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // 2) AudioSource 위치를 마우스 위치로 이동
        // 클릭용 클립 가져오기
        var entry = stateClips.Find(sc => sc.state == FSXState.Click);
        Debug.Log($"[ClickEvent] target={entry.target}, clip={entry.cllip}");
        if (entry.cllip == null)
        {
            Debug.LogWarning("Click 상태에 클립이 할당되어 있지 않습니다.");
            return;
        }
        // 3) 상태에 맞는 클립 재생 (FSXState.Click)
        PlayStateClip(entry.target, entry.state, false);
    }
    public void ObjectAudioPlay(bool isAudioPlaying)
    {

    }


}
