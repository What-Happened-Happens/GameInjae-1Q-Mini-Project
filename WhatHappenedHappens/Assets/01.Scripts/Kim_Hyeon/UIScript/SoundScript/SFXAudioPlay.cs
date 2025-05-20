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

    }

    public void ClickEventAudioPlay()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = clickSoundDepth;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // 2) AudioSource ��ġ�� ���콺 ��ġ�� �̵�
        // Ŭ���� Ŭ�� ��������
        var entry = stateClips.Find(sc => sc.state == FSXState.Click);
        Debug.Log($"[ClickEvent] target={entry.target}, clip={entry.cllip}");
        if (entry.cllip == null)
        {
            Debug.LogWarning("Click ���¿� Ŭ���� �Ҵ�Ǿ� ���� �ʽ��ϴ�.");
            return;
        }
        // 3) ���¿� �´� Ŭ�� ��� (FSXState.Click)
        PlayStateClip(entry.target, entry.state, false);
    }
    public void ObjectAudioPlay(bool isAudioPlaying)
    {

    }


}
