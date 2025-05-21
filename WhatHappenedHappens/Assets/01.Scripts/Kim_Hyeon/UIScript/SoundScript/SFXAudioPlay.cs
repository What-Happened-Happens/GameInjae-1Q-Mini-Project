using System.Linq;
using UnityEngine;

public class SFXAudioPlay : SFXAudioManager
{

    [Header("Short or Long Threshold")]
    [SerializeField] private float shortThreshold = 1f;

    [Header("재생 기준 시간")]
    [SerializeField] private float clickSoundDepth = 1.5f;

    [Header("AudioSourceTest")]
    [SerializeField] private AudioSource audioSource; // 특정 오브젝트 소리를 출력해보기 위한 AudioSource 
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (Input.GetMouseButtonDown(0))
                ClickEventAudioPlay();

        ObjectAudioPlay(true);
        LogOnlySpecificObjectAudio(audioSource);
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
                  $" Click clip =   {entry.clip.ToString()}");
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

        ObjectEventAudioPlay(isAudioLoop);

    }
    public void ObjectEventAudioPlay(bool isLoop)
    {
        var entry = stateClips.Find(sc => sc.state == SFXState.OBJECT);
        Debug.Log($" 등록된 Object 상태의 AudioSource를 찾습니다." +
                  $" Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =       {entry.state.ToString()}," +
                  $" Object clip =        {entry.clip.ToString()}");

        if (entry.clip == null)
        {
            Debug.LogWarning("Object 상태에 클립이 할당되지 않았습니다.");
            return;
        }

        Debug.Log($" 등록된 Object 상태의 AudioSource에서 재생합니다." +
                  $" Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =       {entry.state.ToString()}," +
                  $" Object clip =        {entry.clip.ToString()}");

        SFXAudioManager.Instance.PlayStateClip(entry.targetOutput, SFXState.OBJECT, true, false);
    }

    // 같은 오브젝트 타입인데, 오디오 소스에 들어가는 오브젝트가 다를 때 사용합니다. 
    // 이벤트가 발생한 오브젝트에 따라 다른 오디오 소스에서 재생할 수 있습니다. 
    public void LogOnlySpecificObjectAudio(AudioSource targetSource)
    {
        // state == OBJECT 이고, targetOutput이 specificSource인 항목만 지정해서 출력 
        var entries = stateClips
            .Where(sc => sc.state == SFXState.OBJECT && sc.targetOutput == targetSource);

        foreach (var entry in entries)
        {
            Debug.Log($"[OBJECT only] Source: {entry.targetOutput.name.ToString()}, Clip: {entry.clip.name.ToString()}");
            entry.targetOutput.Play();
        }
    }

    public void OnMuteVolume()
    {
        Debug.Log($"SFXAudioPlay : SFX 음소거 하겠습니다.");
        float entryVolume;

        foreach (var entry in stateClips)
        {
            entryVolume = entry.targetOutput.volume;

            if (entry.targetOutput != null)
            {
                entry.targetOutput.mute = true;
                Debug.Log($"AudioSource : {entry.targetOutput.ToString()} " +
                        $"SFX Volume :    {entry.targetOutput.volume} ");
                Debug.Log($"SFX State :   {entry.state.ToString()} " +
                          $"SFX Clip :    {entry.clip.ToString()} " +
                          $"SFX Mute :    {entry.targetOutput.mute}");
            }
            
        }
    }

}


