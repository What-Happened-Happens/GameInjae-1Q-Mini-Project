using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Linq;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;
            }
            return instance;
        }
    }

    [Header("Audio Source")]
    [SerializeField] protected AudioSource _BGMaudioSource;
    // [SerializeField] protected AudioSource _SFXaudioSource;

    [Header("Sliders")]
    [SerializeField] protected Slider _BGMAudioSlider;
    [SerializeField] protected Slider _SFXAudioSlider;

    [Header("AudioValueText")]
    [SerializeField] protected TMP_Text _BGMaudioText;
    [SerializeField] protected TMP_Text _SFXaudioText;

    private SFXAudioPlay _sfxAudioPlay;
    private SFXAudioManager _sfxAudioManager; 

    // 스테이지 클리어 여부 확인용 test
    public bool isStageCleard = false;

    protected bool _isMute = false;             // 사운드 플레이 중인지 확인하기 위한 변수 

    protected float _currentSliderValue;        // 현재 슬라이더 값 
    protected float _currentSourceVolume;       // 현재 오디오 소스 볼륨

    protected float _PrevBGMSoundValue;         // 이전 BGM사운드 값 
    protected float _PrevSFXaudioValue;         // 이전 SFX 사운드 값

    //SFX 
    protected float _currentSFXsliderValue;     // 현재 SFX 슬라이더 값
    protected float _currentSFXsourceVolume;    // 현재 SFX 오디오 소스 볼륨 


    //배경음은 씬 전환이 되더라도 남도록.   
    protected async void Awake()
    {
        _sfxAudioPlay = FindObjectOfType<SFXAudioPlay>();
        _sfxAudioManager = FindObjectOfType<SFXAudioManager>();

        if (_sfxAudioManager == null) Debug.LogError("SFXAudioManager 할당되지 않았습니다.");
        if (_sfxAudioPlay == null) Debug.LogError("SFXAudioManager 할당되지 않았습니다.");
        if (_BGMaudioSource == null) Debug.LogError("AudioSource 할당되지 않았습니다.");
        if (_BGMAudioSlider == null) Debug.LogError("AudioSlider 할당되지 않았습니다.");
        if (_BGMaudioText == null) Debug.LogError("VolumeText  할당되지 않았습니다.");

        // BGM
        float saveCurrentAudio = await AudioLoad("save_BGMSoundVolume", 10f);
        _BGMaudioSource.volume = saveCurrentAudio;
        _BGMAudioSlider.value = saveCurrentAudio;
        _PrevBGMSoundValue = saveCurrentAudio;

        _isMute = PlayerPrefs.GetInt("save_IsMuted", 0) == 1;
        if (_isMute)
            await ApplyMuteAsync();

        // SFX 
        float savedSFXvolume = await AudioLoad("save_SFXSoundVolume", 10f);
        _SFXAudioSlider.value = savedSFXvolume;
        _PrevSFXaudioValue = savedSFXvolume; 
    }   

    // 음소거 
    public async void OnClickMuteButton()  // 음소거 버튼을 눌렀을 때
    {
        if (_isMute && isStageCleard) // 음소거 상태이고, 스테이지를 클리어 했을 때 
        {
            await ApplyMuteAsync();
            isMute(false);
        }
        else 
        {
            await ApplyMuteAsync();
            isMute(true);
        }       

    }

    public async Task ApplyMuteAsync()
    {
        if (_isMute) return;

        // 현재 볼륨을 저장해두고
        _currentSourceVolume = _BGMaudioSource.volume;

        // 비동기 저장
        await AudioSave("save_BGMSoundVolume", _currentSourceVolume);

        // BGM 음소거
        _BGMaudioSource.volume = 0f;
        _BGMAudioSlider.value = 0f;
        Debug.Log($"BGM 음향을 음소거 시킵니다. 현 상태 : {_isMute}");

        // SFX 음소거 
        _sfxAudioPlay.OnMuteVolume(); 
    }

    // ------------------------------------------ 비동기 데이터 저장 및 로드 ------------------------------------//
    public async Task<float> AudioSave(string prefKey, float SaveData) // 슬라이더 변경점에 따라서 그 당시의 값을 저장 
    {
        await Task.Yield();

        PlayerPrefs.SetFloat(prefKey, SaveData);
        PlayerPrefs.Save();

        Debug.Log($"레포지토리에 저장했습니다. KeyName : {prefKey}, KeyValue : {SaveData}");
        return SaveData;
    }

    public async Task<float> AudioLoad(string prefKey, float LoadData = 0f) // 그리고, 저장된 값을 불러와서 읽는다. 
    {
        // 값을 다시 사용하기 위해서 파라메터를 사용       
        try
        {
            await Task.Yield();
            Debug.Log($"레포지토리에 불러옵니다. KeyName : {prefKey}, KeyValue : {LoadData}");
        }
        catch (Exception e)
        {
            Debug.LogError($"SoundValueLoad 중 예외 발생: {e}");
            throw e;
            // Error 출력
        }

        return PlayerPrefs.GetFloat(prefKey, LoadData);
    }

    public bool isMute(bool soundState) // 음소거 가 풀리면, 저장된 값을 다시 불러와서 그 순간 부터 재생. 
    {
        if (soundState)
        {
            Debug.Log($"현재 음소거 상태 일 때 : {soundState}");
            _isMute = false;
            return _isMute;
        }
        else
        {
            Debug.Log($"현재 음소거 상태가 아닐 때 : {soundState}");
            _isMute = true;
            return _isMute;
        }

    }

}
