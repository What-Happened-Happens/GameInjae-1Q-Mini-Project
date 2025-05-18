using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Threading.Tasks;

public class SoundManager : MonoBehaviour, IPointerDownHandler
{
    public static SoundManager Instance;

    [Header("Audio Mixer & Sliders")]
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private Slider AudioSlider;
    public TMP_Text soundvalueText;

    private bool _isPlaying = false;    // 사운드 플레이 중인지 확인하기 위한 변수 
    private bool _isSave = false;       // 저장된 데이터가 있는 지 확인하기 위한 변수

    private float _currentSliderValue;
    private float _currentSourceValue;
    private Task<float> _currentSourceValueTask; 
    private Task<float> _CurrentSoundValueTask;   // 슬라이더를 연결할 현재 사운드 값    
    private Task<float> _PrevSoundValueTask;      // 이전 사운드 값 

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        Instance = this;
        AudioSource = gameObject.GetComponent<AudioSource>(); // 이 스크립트를 가진 게임 오브젝트의 AudioSource 를 가져온다. 

        AudioSlider = GetComponent<UnityEngine.UI.Slider>();
        AudioSlider.minValue = 0;
        AudioSlider.maxValue = 100;

        try
        {
            _isSave = PlayerPrefs.HasKey("save_CurrentSoundValue");

            if (!_isSave)
            {
                _currentSliderValue = AudioSlider.value;    // 현재 사운드 슬라이더 값 
                _currentSourceValue = AudioSource.volume;   // 현재 사운드 값
            }
            else
            {
                Debug.Log($"저장되어 있는 데이터가 있습니다.");
                _CurrentSoundValueTask = SoundValueLoad("save_CurrentSoundValue");
                _currentSourceValueTask = SoundValueLoad("save_CurrentSourceValue");
                _PrevSoundValueTask = null; 
            }

        }
        catch (Exception e)
        {
            Debug.LogError($"SoundValueInitialize 중 예외 발생 : {e}");
            throw e;
            // Error 출력
        }

        AudioSlider.onValueChanged.AddListener(delegate { onValueChanged(); });
    }

    public void OnClickeMuteButton() // 음소거 버튼을 눌렀을 때 
    {
        // 음소거 버튼을 눌렀을 때, 
        // 슬라이더 현재 슬라이더 값을 받아서, 저장 하고,  

        // 만약에 음소거 버튼을 눌렀다면, 현재 슬라이더 값과 AudioSource의 값을 0으로 변환. 
        // 현재 슬라이더 값을 이전 슬라이더값 변수로 저장
        // 음소거 버튼이 눌린 상태에서, 만약 현제 슬라이더 값이 이전 슬라디어 값과 달라지게 된다면,
        // 다시 저장했던 현재 슬라이더 값을 불러와서 슬라이더 값에 할당함. 
        // 그리고 그 값에서부터 슬라이더 변경에 따라서 오디오 볼륨이 조절되어야 한다.  

        if (_isPlaying) return;

        // 현재 슬라이더 값을 저장
        var SaveCurrentSound = SoundValueSave("save_CurrentSoundValue", _currentSliderValue);     
        // 현재 오디오 소스 값을 저장 
        var SaveCurrentSoundSlider = SoundValueSave("save_CurrentSliderValue", _currentSourceValue);

        _isPlaying = false;     // 논 플레이 
        _currentSourceValue = 0f; 

    }

    public async Task<float> SoundValueSave(string prefKey, float SaveData) // 슬라이더 변경점에 따라서 그 당시의 값을 저장 
    {
        float result = await Task.Run(() =>
        {
            // saved_SoundValue 라는 Key 값으로 SaveData 를 저장 
            // 여기서 SaveData 는 음소거 버튼을 눌렀을 때,
            // 현재 음량을 뜻하는 슬라이더 값의 키값 

            PlayerPrefs.SetFloat(prefKey, SaveData);
            return SaveData;
        });

        return result;
    }

    public async Task<float> SoundValueLoad(string prefKey, float LoadData = 0f) // 그리고, 저장된 값을 불러와서 읽는다. 
    { // 값을 다시 사용하기 위해서 파라메터를 사용 
        try
        {
            // 기본적 실행 
            float result = await Task.Run(() =>
            {
                PlayerPrefs.GetFloat(prefKey, LoadData);
                return LoadData; // 키가 없을 때 반환할 기본 음량
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"SoundValueLoad 중 예외 발생: {e}");
            throw e;
            // Error 출력
        }
        finally
        {
            // 오류가 발생하건 아니건 , 실행.
            Debug.Log("프로그램 종료");
        }

        return PlayerPrefs.GetFloat(prefKey, LoadData);
    }

    public bool isMute(bool soundState) // 음소거 가 풀리면, 저장된 값을 다시 불러와서 그 순간 부터 재생. 
    {
        return soundState;
    }

    public void onValueChanged()
    {
        Debug.Log($"사운드 텍스트 값 변경 {AudioSlider.value}");
        int soundvalue = (int)AudioSlider.value;
        soundvalueText.text = soundvalue.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ((IPointerDownHandler)AudioSlider).OnPointerDown(eventData);
        AudioSlider = eventData.selectedObject.GetComponent<Slider>();
        onValueChanged();
    }

}
