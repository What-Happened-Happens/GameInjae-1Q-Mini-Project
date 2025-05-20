using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensure FSXState is public to match the accessibility of the method using it
public enum FSXState
{
    None = 0,
    Click = 1,
}

public class FSXAudioManager : AudioManager
{
    public static FSXAudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _audioTarget;
    [SerializeField] private string clipPath = "Sounds/";
    [SerializeField] private float longPressThreshold = 0.5f;

    private Coroutine _stopRoutine;

    public float shortDuration = 0.3f;
    public float longDuration = 1f;
    public float volumeScale = 0.5f;

    [Header("AudioClips")]
    public List<AudioClip> clips = new List<AudioClip>();

    private float _pressStartTime;
    private bool _isPressing = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if (_audioTarget != null)
            _audioSource = _audioTarget.GetComponent<AudioSource>();

        if (_audioSource == null)
            Debug.LogError("FSXAudioManager: AudioSource 할당이 되지 않았습니다.");

        _audioSource.volume = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isPressing = true;
            _pressStartTime = Time.time;
        }

        if (_isPressing && Input.GetMouseButtonUp(0))
        {
            _isPressing = false;
            float heldDuration = Time.time - _pressStartTime;

            bool isShort = heldDuration < longPressThreshold;
            float duration = isShort ? shortDuration : longDuration;

        }
    }

    public void PlayClipWithDuration(AudioClip clip, float duration, float volume)
    {
        if (_audioSource.clip == null)
        {
            Debug.LogWarning("FSXAudioManager: 재생할 AudioClip이 없습니다.");
            return;
        }

        if (_stopRoutine != null)
        {
            Debug.Log($"이미 코루틴 이 진행되고 있습니다.");
            StopCoroutine(_stopRoutine);
        }

        _audioSource.clip = clip;
        _audioSource.volume = volume;
        _audioSource.Play();

        _stopRoutine = StartCoroutine(StopAfterDuration(duration));
    }

    public void PlayClipChanged(FSXState state, bool isShort)
    {
        AudioClip clipToPlay = state switch 
        {
            FSXState.Click => clips.Find(c => c.name.Contains("Click")),
            // FSXState.Drag  => clips.Find(c => c.name.Contains("Drag")),
            _ => null
        };
        float duration = isShort ? shortDuration : longDuration;
        PlayClipWithDuration(clipToPlay, duration, volumeScale);
    }

    private IEnumerator StopAfterDuration(float duration)
    {
        float waitTime = Mathf.Min(duration, _audioSource.clip.length);
        yield return new WaitForSeconds(waitTime);

        _audioSource.Stop();
        _stopRoutine = null;
    }
}
