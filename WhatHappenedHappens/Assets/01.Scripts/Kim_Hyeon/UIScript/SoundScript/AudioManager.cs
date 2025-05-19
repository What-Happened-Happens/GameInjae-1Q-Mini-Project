using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading.Tasks;


public class AudioManager : MonoBehaviour
{

    [Header("Audio Mixer & Sliders")]
    [SerializeField] protected AudioSource AudioSource;
    [SerializeField] protected GameObject AudioTarget;
    [SerializeField] protected Slider AudioSlider;
    [SerializeField] protected TMP_Text soundvalueText;

    protected bool _isPlaying = false;          // 사운드 플레이 중인지 확인하기 위한 변수 
    private bool _isSave = false;               // 저장된 데이터가 있는 지 확인하기 위한 변수

    protected float _currentSliderValue;        // 현재 슬라이더 값 
    protected float _currentSourceVolume;       // 현재 오디오 소스 볼륨 
    protected float _PrevSoundValue;        // 이전 사운드 값 

    private Task<float> SaveCurrentSoundTask;   // 현재 슬라이더 값을 레지포트리에 저장할 변수 
    private Task<float> SavePrevSoundTask;      // 이전 슬라이더 값을 레지포트리에 저장할 변수 

    protected async void Start()
    {
        //AudioTarget = GetComponent<GameObject>();
        //AudioSource = AudioTarget.GetComponent<AudioSource>();

        float saved = await SoundValueLoad("save_CurrentSoundValue", 0f);
        AudioSource.volume = saved;
        AudioSlider.value = saved;
        _PrevSoundValue = saved;
        
    }

    public async void OnClickMuteButton()  // 음소거 버튼을 눌렀을 때
    {
        if (_isPlaying) return; 

        // 현재 볼륨을 저장해두고
        _currentSourceVolume = AudioSource.volume;

        // 비동기 저장
        await SoundValueSave("save_CurrentSoundValue", _currentSourceVolume);

        isMute(false);
        // 텍스트 반영 
        soundvalueText.text = "X";
        // 실제 음소거
        AudioSource.volume = 0f;
        AudioSlider.value = 0f;
        Debug.Log($"음향을 음소거 시킵니다. 현 상태 : {_isPlaying}");
    } 

    public async Task<float> SoundValueSave(string prefKey, float SaveData) // 슬라이더 변경점에 따라서 그 당시의 값을 저장 
    {
        await Task.Yield();

        PlayerPrefs.SetFloat(prefKey, SaveData);
        PlayerPrefs.Save();

        Debug.Log($"레지포토리에 저장했습니다. KeyName : {prefKey}, KeyValue : {SaveData}");
        return SaveData;
    }

    public async Task<float> SoundValueLoad(string prefKey, float LoadData = 0f) // 그리고, 저장된 값을 불러와서 읽는다. 
    { // 값을 다시 사용하기 위해서 파라메터를 사용       
        try
        {
            await Task.Yield();
            Debug.Log($"레지포토리에 불러옵니다. KeyName : {prefKey}, KeyValue : {LoadData}");
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
        if (soundState) {
            Debug.Log($"현재 음소거 상태 일 때 : {soundState}");
            _isPlaying = false; 
            return _isPlaying; 
        }
        else
        {
            Debug.Log($"현재 음소거 상태가 아닐 때 : {soundState}");
            _isPlaying = true;
            return _isPlaying;
        }

    }



}
