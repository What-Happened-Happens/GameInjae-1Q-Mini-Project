using System.Linq;
using UnityEngine;

public class SFXAudioPlay : SFXAudioManager
{

    [Header("Short or Long Threshold")]
    [SerializeField] private float shortThreshold = 1f;

    [Header("��� ���� �ð�")]
    [SerializeField] private float clickSoundDepth = 1.5f;

    [Header("AudioSourceTest")]
    [SerializeField] private AudioSource audioSource; // Ư�� ������Ʈ �Ҹ��� ����غ��� ���� AudioSource 
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
        Debug.Log($"������ �Է� ������ : X : {screenPos.x}" +
                                       $"Y : {screenPos.y}" +
                                       $"Z : {screenPos.z}");
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        Debug.Log($"ī�޶� ��ũ���� Ŭ�� ��ġ�� ���� �����̽� ���������� ��ȯ");

        var entry = stateClips.Find(sc => sc.state == SFXState.CLICK);
        Debug.Log($"��ϵ� Click ������ AudioSource�� ã���ϴ�. Click AudioSource : {entry.targetOutput.ToString()}" +
                  $" Click State =  {entry.state.ToString()}, " +
                  $" Click clip =   {entry.clip.ToString()}");
        if (entry.clip == null)
        {
            Debug.LogWarning("Click ���¿� Ŭ���� �Ҵ���� �ʾҽ��ϴ�.");
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
        Debug.Log($" ��ϵ� Object ������ AudioSource�� ã���ϴ�." +
                  $" Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =       {entry.state.ToString()}," +
                  $" Object clip =        {entry.clip.ToString()}");

        if (entry.clip == null)
        {
            Debug.LogWarning("Object ���¿� Ŭ���� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        Debug.Log($" ��ϵ� Object ������ AudioSource���� ����մϴ�." +
                  $" Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =       {entry.state.ToString()}," +
                  $" Object clip =        {entry.clip.ToString()}");

        SFXAudioManager.Instance.PlayStateClip(entry.targetOutput, SFXState.OBJECT, true, false);
    }

    // ���� ������Ʈ Ÿ���ε�, ����� �ҽ��� ���� ������Ʈ�� �ٸ� �� ����մϴ�. 
    // �̺�Ʈ�� �߻��� ������Ʈ�� ���� �ٸ� ����� �ҽ����� ����� �� �ֽ��ϴ�. 
    public void LogOnlySpecificObjectAudio(AudioSource targetSource)
    {
        // state == OBJECT �̰�, targetOutput�� specificSource�� �׸� �����ؼ� ��� 
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
        Debug.Log($"SFXAudioPlay : SFX ���Ұ� �ϰڽ��ϴ�.");
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


