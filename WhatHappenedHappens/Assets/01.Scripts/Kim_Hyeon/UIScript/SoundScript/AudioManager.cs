using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading.Tasks;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour //, IPointerDownHandler
{

    [Header("Audio Mixer & Sliders")]
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private GameObject AudioTarget;
    [SerializeField] private Slider AudioSlider;
    [SerializeField] private TMP_Text soundvalueText;

    private bool _isPlaying = false;    // 사운드 플레이 중인지 확인하기 위한 변수 
    private bool _isSave = false;       // 저장된 데이터가 있는 지 확인하기 위한 변수

    private float _currentSliderValue;       // 현재 슬라이더 값 
    private float _currentSourceVolume;      // 현재 오디오 소스 볼륨 
    private float _PrevSoundValueTask;       // 이전 사운드 값 

    private Task<float> SaveCurrentSoundTask; // 현재 슬라이더 값을 레지포트리에 저장할 변수 
    private Task<float> SavePrevSoundTask;    // 이전 슬라이더 값을 레지포트리에 저장할 변수 

    private async void Start()
    {
   

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
        if (!_isPlaying ) return;

        // 비동기로 현재 값으로 불러온다.
        var curr_volume = await SoundValueLoad("save_CurrentSoundValue", _currentSourceVolume);       

        _isPlaying = true;
        AudioSource.volume = curr_volume;
        AudioSlider.value = curr_volume;
        soundvalueText.text = curr_volume.ToString();
    }
    //public void OnClickeMuteButton() // 음소거 버튼을 눌렀을 때 
    //{
    //    // 음소거 버튼을 눌렀을 때, 
    //    // 슬라이더 현재 슬라이더 값을 받아서, 저장 하고,  

    //    // 만약에 음소거 버튼을 눌렀다면, 현재 슬라이더 값과 AudioSource의 값을 0으로 변환. 
    //    // 현재 슬라이더 값을 이전 슬라이더값 변수로 저장

    //    Debug.Assert(_isPlaying, $"플레이 중에 있습니다. _isPlaying : {_isPlaying}");
    //    Debug.Assert(AudioSource.volume == 0f, $"음향 현재 볼륨 : {AudioSource.volume}");

    //    // 현재 슬라이더 값을 저장
    //    SaveCurrentSoundTask = SoundValueSave("save_CurrentSoundValue", _currentSliderValue);
    //    _isPlaying = false;         // 플레이 할 수 없게 지정 
    //    _currentSourceVolume = 0f;  // 현재 오디오 소스 볼륨을 음소거.
    //    soundvalueText.text = "X";

    //    // 현재 사운드 값을 초기화 
    //    //_CurrentSoundValueTask = null;
    //    var currentSound = SoundValueLoad("save_CurrentSoundValue"); // 현재 사운드 값을 이전 사운드 값으로 저장 
    //    SavePrevSoundTask = currentSound;
    //}

    //public void OnClearMuteButton()
    //{
    //    // 만약 현재 슬라이더 값이 이전 슬라디어 값과 달라지게 된다면,
    //    if (!_isPlaying && AudioSource.volume == 0f) return;

    //    if (SaveCurrentSoundTask == SavePrevSoundTask) return; // 현재 사운드 값이 이전 값과 다를 때 

    //    // 다시 저장했던 현재 슬라이더 값을 불러와서 슬라이더 값에 할당함. 
    //    // 그리고 그 값에서부터 슬라이더 변경에 따라서 오디오 볼륨이 조절되어야 한다.      
    //    AudioSlider.value = AudioSource.volume;
    //    AudioSource.volume = AudioSlider.value;
    //    _isPlaying = true;
    //    soundvalueText.text = AudioSource.volume.ToString();
    //}

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
        finally
        {

        }

        return PlayerPrefs.GetFloat(prefKey, LoadData);
    }

    public bool isMute(bool soundState) // 음소거 가 풀리면, 저장된 값을 다시 불러와서 그 순간 부터 재생. 
    {
        return soundState;
    }

    //public void onValueChanged()
    //{
    //    Debug.Log($"사운드 텍스트 값 변경 {AudioSlider.value}");
    //    int soundvalue = (int)AudioSlider.value;
    //    soundvalueText.text = soundvalue.ToString();
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    ((IPointerDownHandler)AudioSlider).OnPointerDown(eventData);
    //    AudioSlider = eventData.selectedObject.GetComponent<Slider>();
    //    onValueChanged();
    //}

}
