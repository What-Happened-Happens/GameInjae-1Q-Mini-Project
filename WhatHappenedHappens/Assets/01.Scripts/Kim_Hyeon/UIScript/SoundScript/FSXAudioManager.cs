using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FSXAudioManager : MonoBehaviour
{
    public static FSXAudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private string clipPath = "06.Sounds/";
    
    private readonly Dictionary<string, AudioClip> _cache = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(_audioSource.gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private async void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"효과음 테스트용"); 
            _ =  FSXAudioManager.Instance.PlaySFXAsync("Satin Black");
        }
    }
  
    private async Task PlaySFXAsync(string clipName)
    {
        AudioClip clip;
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
        _audioSource.PlayOneShot(clip, 1f);   
    }

    private async Task<AudioClip> LoadClipAsync(string clipName)
    {
        var req = Resources.LoadAsync<AudioClip>(clipPath + clipName);
        while (!req.isDone)
        {
            await Task.Yield();
        }

        if (req.asset is AudioClip ac) return ac;
        Debug.LogWarning($"[{nameof(FSXAudioManager)}] Can't load: {clipName}");
        return req.asset as AudioClip;
    }
}
