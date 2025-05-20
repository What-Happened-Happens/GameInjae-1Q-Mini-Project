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

        isStageClear = true; 
        ObjectAudioPlay(true); 
    }

    public void ClickEventAudioPlay()
    {
        Debug.Log($"Object Audio State : Click Play");
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = clickSoundDepth;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        Debug.Log($"카메라 스크린을 클릭 위치를 월드 스페이스 포지션으로 변환");

        var entry = stateClips.Find(sc => sc.state == SFXState.Click);
        Debug.Log($"등록된 Object 상태의 AudioSource를 찾습니다. Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =  {entry.state.ToString()}, " +
                  $" Object clip =  {entry.clip.ToString()}");
        if (entry.clip == null)
        {
            Debug.LogWarning("Click 상태에 클립이 할당되지 않았습니다.");
            return;
        }

        PlayStateClip(entry.targetOutput, entry.state, false);
    }

    public void ObjectAudioPlay(bool isAudioPlaying)
    {
        Debug.Log($"Object Audio State : Object Play"); 
        if (isStageClear)
        {
            ObjectEventAudioPlay();
        }
        else if (!isStageClear)
        {

        }
    }
    // 오브젝트의 오디오를 플레이. 
    // 추후, 스테이지 클리어 상태에 따라서 오브젝트 오디오 볼륨 조절 
    public void ObjectEventAudioPlay()
    {
        var entry = stateClips.Find(sc => sc.state == SFXState.Object);
        Debug.Log($"등록된 Object 상태의 AudioSource를 찾습니다. Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =  {entry.state.ToString()}, " +
                  $" Object clip =  {entry.clip.ToString()}");
        if (entry.clip == null)
        {
            Debug.LogWarning("Object 상태에 클립이 할당되지 않았습니다.");
            return;
        }

        Debug.Log($" 등록된 Object 상태의 AudioSource에서 재생합니다." +
                  $" Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =  {entry.state.ToString()}," +
                  $" Object clip =  {entry.clip.ToString()}");
        PlayStateClip(entry.targetOutput, SFXState.Object, isClipLength);
    }

}
