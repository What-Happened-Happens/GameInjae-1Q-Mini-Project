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
            if (Input.GetMouseButtonDown(0))
            ClickEventAudioPlay();

        ObjectAudioPlay(true);
    }

    public void ClickEventAudioPlay()
    {
        Debug.Log($"current Audio State : Click Play");

        Vector3 screenPos = Input.mousePosition;
        screenPos.z = clickSoundDepth;
        Debug.Log($"포인터 입력 포지션 : X : {screenPos.x}" +
                  $"Y : {screenPos.y}" +
                  $"Z : {screenPos.z}");
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        Debug.Log($"카메라 스크린을 클릭 위치를 월드 스페이스 포지션으로 변환");

        var entry = stateClips.Find(sc => sc.state == SFXState.CLICK);
        Debug.Log($"등록된 Click 상태의 AudioSource를 찾습니다. Click AudioSource : {entry.targetOutput.ToString()}" +
                  $" Click State =  {entry.state.ToString()}, " +
                  $" Click clip =  {entry.clip.ToString()}");
        if (entry.clip == null)
        {
            Debug.LogWarning("Click 상태에 클립이 할당되지 않았습니다.");
            return;
        }

        SFXAudioManager.Instance.PlayStateClip(entry.targetOutput, entry.state, true, false);
    }

    public void ObjectAudioPlay(bool isAudioLoop)
    {
        Debug.Log($"Object Audio State : Object Play");

        ObjectEventAudioPlay(true);

    }
    public void ObjectEventAudioPlay(bool isLoop)
    {
        var entry = stateClips.Find(sc => sc.state == SFXState.OBJECT);
        Debug.Log($" 등록된 Object 상태의 AudioSource를 찾습니다." +
                  $" Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =  {entry.state.ToString()}," +
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
        SFXAudioManager.Instance.PlayStateClip(entry.targetOutput, SFXState.OBJECT, true, false);
    }

    public void OnMuteVolume()
    {
        Debug.Log($"SFXAudioPlay : SFX 음소거 하겠습니다.");

        foreach (var entry in stateClips)
        {
            if (entry.targetOutput != null)
            {
                entry.targetOutput.mute = true;
                Debug.Log($"AudioSource : {entry.targetOutput.ToString()} " +
                        $"SFX Volume : {entry.targetOutput.volume} ");
                Debug.Log($"SFX State :  {entry.state.ToString()} " +
                          $"SFX Clip :   {entry.clip.ToString()} " +
                          $"SFX Mute : {entry.targetOutput.mute}");
            }
            else if (!isMute() && Input.GetMouseButtonDown(0))
            {
                entry.targetOutput.mute = false;
                SFXAudioManager.Instance.volumeScale = 0.5f;
            }


        }

    }
}

