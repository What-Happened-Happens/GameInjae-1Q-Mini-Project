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
    [SerializeField] protected TMP_Text AudioValueText;
    public static AudioManager Instance { get; private set; }
    protected bool _isMute = false;             // 사운드 플레이 중인지 확인하기 위한 변수 

    protected float _currentSliderValue;        // 현재 슬라이더 값 
    protected float _currentSourceVolume;       // 현재 오디오 소스 볼륨 
    protected float _PrevSoundValue;            // 이전 사운드 값 

    protected async void Start()
    {
        if (AudioSource == null)    Debug.LogError("AudioSource 할당 안 됨!");
        if (AudioSlider == null)    Debug.LogError("AudioSlider 할당 안 됨!");
        if (AudioValueText == null) Debug.LogError("VolumeText 할당 안 됨!");

        float saveCurrentAudio = await AudioLoad("save_CurrentSoundValue", 10f);
        AudioSource.volume = saveCurrentAudio;
        AudioSlider.value = saveCurrentAudio;
        _PrevSoundValue = saveCurrentAudio;
    }

    public async void OnClickMuteButton()  // 음소거 버튼을 눌렀을 때
    {
        if (_isMute) return; 

        // 현재 볼륨을 저장해두고
        _currentSourceVolume = AudioSource.volume;

        // 비동기 저장
        await AudioSave("save_CurrentSoundValue", _currentSourceVolume);

        isMute(false);
       
        // 실제 음소거
        AudioSource.volume = 0f;
        AudioSlider.value = 0f;
        Debug.Log($"음향을 음소거 시킵니다. 현 상태 : {_isMute}");
    } 

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
        if (soundState) {
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
