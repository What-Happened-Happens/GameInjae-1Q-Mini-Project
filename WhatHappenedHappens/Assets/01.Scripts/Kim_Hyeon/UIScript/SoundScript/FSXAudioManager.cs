using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FSXAudioManager : MonoBehaviour
{
    public static FSXAudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _audioTarget;
    [SerializeField] private string clipPath = "Sounds/";

    private readonly Dictionary<string, AudioClip> _cache = new();

    public float duration = 0.5f;
    public float volumeScale = 0.5f; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else                  Destroy(_audioSource.gameObject);
        DontDestroyOnLoad(gameObject);

        if (_audioTarget == null)
        {
            Debug.LogError("FSXAudioManager: _audioTarget이 할당되지 않았습니다.");
        }

        _audioSource = _audioTarget.GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("FSXAudioManager: _audioTarget 에 AudioSource 가 없습니다.");
        }
        _audioSource.volume = 0f; 
    }

    private async void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"효과음 테스트 클릭!");
     
           await PlayAssignedClipAsync(duration, volumeScale);
        }        
    }

    public async Task PlayAssignedClipAsync(float duration, float volumeScale)
    {
        if (_audioSource == null || _audioSource.clip == null)
        {
            Debug.LogWarning("FSXAudioManager: 재생할 AudioSource 또는 clip이 없습니다.");
            return;
        }

        _audioSource.volume = volumeScale;
        _audioSource.Play();

        await Task.Delay(TimeSpan.FromSeconds(duration));
        await Task.Yield();

        ClipStop();
    }

    public void ClipStop()
    {
        _audioSource.Stop();
    }
   
}
