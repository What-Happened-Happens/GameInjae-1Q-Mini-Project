using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading.Tasks;


public class AudioManager : MonoBehaviour
{

    [Header("Audio Mixer & Sliders")]
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private GameObject AudioTarget;
    [SerializeField] private Slider AudioSlider;
    [SerializeField] private TMP_Text soundvalueText;

    protected bool _isPlaying = false;          // 사운드 플레이 중인지 확인하기 위한 변수 
    private bool _isSave = false;               // 저장된 데이터가 있는 지 확인하기 위한 변수

    protected float _currentSliderValue;        // 현재 슬라이더 값 
    protected float _currentSourceVolume;       // 현재 오디오 소스 볼륨 
    protected float _PrevSoundValueTask;        // 이전 사운드 값 

    private Task<float> SaveCurrentSoundTask;   // 현재 슬라이더 값을 레지포트리에 저장할 변수 
    private Task<float> SavePrevSoundTask;      // 이전 슬라이더 값을 레지포트리에 저장할 변수 

    private async void Start()
    {
        AudioTarget = GetComponent<GameObject>();
        AudioSource = AudioTarget.GetComponent<AudioSource>();

        float saved = await SoundValueLoad("save_CurrentSoundValue", 0f);
        AudioSource.volume = saved;
        AudioSlider.value = saved;
        _PrevSoundValueTask = saved;

        AudioSlider.onValueChanged.AddListener(async val =>
        {
            AudioSource.volume = val;

            await SoundValueSave("save_CurrentSoundValue", val);
            _PrevSoundValueTask = val;
        });

        try
        {
            if (!_isSave)
            {
                _currentSliderValue = AudioSlider.value;
                _currentSourceVolume = AudioSource.volume;
            }
            else
            {
                Debug.Log($"저장되어 있는 데이터가 있습니다.");
                _currentSliderValue = PlayerPrefs.GetFloat("save_CurrentSoundValue");
                _currentSourceVolume = PlayerPrefs.GetFloat("save_CurrentSourceVolume");
                _PrevSoundValueTask = 0f;
            }

        }
        catch (Exception e)
        {
            Debug.LogError($"SoundValueInitialize 중 예외 발생 : {e}");
            throw e;
            // Error 출력
        }
        finally
        {
            // 오류가 발생하건 아니건 , 실행.

            Debug.Log("프로그램 종료");
        }
        //AudioSlider.onValueChanged.AddListener(delegate { onValueChanged(); });
    }

    private void Update()
    {
    }
    public async void OnClickMuteButton()  // 음소거 버튼을 눌렀을 때
    {
        // 현재 볼륨을 저장해두고
        _currentSourceVolume = AudioSource.volume;

        // 비동기 저장
        await SoundValueSave("save_CurrentSoundValue", _currentSourceVolume);

        _isPlaying = false;
        // 텍스트 반영 
        soundvalueText.text = "X";
        // 실제 음소거
        AudioSource.volume = 0f;
        AudioSlider.value = 0f;
    }

    public async void OnClearMuteButton()
    {
        if (!_isPlaying) return;

        // 비동기로 현재 값으로 불러온다.
        var curr_volume = await SoundValueLoad("save_CurrentSoundValue", _currentSourceVolume);

        _isPlaying = true;
        AudioSource.volume = curr_volume;
        AudioSlider.value = curr_volume;
        soundvalueText.text = curr_volume.ToString();
    }

    public async Task<float> SoundValueSave(string prefKey, float SaveData) // 슬라이더 변경점에 따라서 그 당시의 값을 저장 
    {
        await Task.Yield();

        PlayerPrefs.SetFloat(prefKey, SaveData);
        PlayerPrefs.Save();

        return SaveData;
    }

    public async Task<float> SoundValueLoad(string prefKey, float LoadData = 0f) // 그리고, 저장된 값을 불러와서 읽는다. 
    { // 값을 다시 사용하기 위해서 파라메터를 사용       
        try
        {
            await Task.Yield();
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
        return soundState;
    }



}
