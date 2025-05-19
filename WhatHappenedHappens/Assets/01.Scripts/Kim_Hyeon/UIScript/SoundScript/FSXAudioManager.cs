using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FSXAudioManager : MonoBehaviour
{
    public static FSXAudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private string clipPath = "Sounds/";
    public AudioClip clip;

    private readonly Dictionary<string, AudioClip> _cache = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(_audioSource.gameObject); 
    }

    public async Task PlaySFXAsync(string clipName)
    {        
        if (!_cache.TryGetValue(clipName , out clip))
        {
            clip = await LoadClipAsync(clipName);    
            if (clip == null)
            {
                Debug.LogWarning($"[FSXAudioManager] SFX '{clipName}' not found in Resources/{clipPath}");
                return;
            }          
            _cache[clipName] = clip;
        }
        _audioSource.loop = false;
        _audioSource.time = 1f;
        _audioSource.volume = 5f;
      
        _audioSource.PlayOneShot(_cache[clipName]);
        _audioSource.Play();
    }

    private Task<AudioClip> LoadClipAsync(string clipName)
    {
        var FSX = new TaskCompletionSource<AudioClip>();
        var req = Resources.LoadAsync<AudioClip>(clipPath + clipName);

        req.completed += _ =>
        {
            FSX.SetResult(req.asset as AudioClip);
        };
        return FSX.Task; 
    }
}
