using UnityEngine;

public class SFXAudioPlay : SFXAudioManager
{
    [Header("Short or Long Threshold")]
    [SerializeField] private float shortThreshold = 1f;

    [Header("��� ���� �ð�")]
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
        Debug.Log($"ī�޶� ��ũ���� Ŭ�� ��ġ�� ���� �����̽� ���������� ��ȯ");

        var entry = stateClips.Find(sc => sc.state == SFXState.Click);
        Debug.Log($"��ϵ� Object ������ AudioSource�� ã���ϴ�. Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =  {entry.state.ToString()}, " +
                  $" Object clip =  {entry.clip.ToString()}");
        if (entry.clip == null)
        {
            Debug.LogWarning("Click ���¿� Ŭ���� �Ҵ���� �ʾҽ��ϴ�.");
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
    // ������Ʈ�� ������� �÷���. 
    // ����, �������� Ŭ���� ���¿� ���� ������Ʈ ����� ���� ���� 
    public void ObjectEventAudioPlay()
    {
        var entry = stateClips.Find(sc => sc.state == SFXState.Object);
        Debug.Log($"��ϵ� Object ������ AudioSource�� ã���ϴ�. Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =  {entry.state.ToString()}, " +
                  $" Object clip =  {entry.clip.ToString()}");
        if (entry.clip == null)
        {
            Debug.LogWarning("Object ���¿� Ŭ���� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        Debug.Log($" ��ϵ� Object ������ AudioSource���� ����մϴ�." +
                  $" Object AudioSource : {entry.targetOutput.ToString()}" +
                  $" Object State =  {entry.state.ToString()}," +
                  $" Object clip =  {entry.clip.ToString()}");
        PlayStateClip(entry.targetOutput, SFXState.Object, isClipLength);
    }

}
