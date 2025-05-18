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
    private float _CurrentSoundValue;   // 현재 사운드 값 
    private float _PrevSoundValue;      // 이전 사운드 값 

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        Instance = this;

        AudioSlider = GetComponent<UnityEngine.UI.Slider>();
        AudioSlider.minValue = 0;
        AudioSlider.maxValue = 100;

        try
        {
            _isSave = PlayerPrefs.HasKey("saved_SoundValue");
            Debug.Assert(!_isSave, $"사운드 값 저장된 데이터가 없습니다.");

            _CurrentSoundValue = 0f;
            _PrevSoundValue = 0f;
        }
        catch (Exception e)
        {
            Debug.LogError($"SoundValueLoad 중 예외 발생: {e}");
            throw e;
            // Error 출력
        }


        AudioSlider.onValueChanged.AddListener(delegate { onValueChanged(); });
    }

    public void Sound()
    {
        // 슬라이더 값
    }


    public async Task<float> SoundValueSave(float SaveData) // 슬라이더 변경점에 따라서 그 당시의 값을 저장 
    {
        float result = await Task.Run(() =>
        {
            // saved_SoundValue 라는 Key 값으로 SaveData 를 저장 
            // 여기서 SaveData 는 음소거 버튼을 눌렀을 때,
            // 현재 음량을 뜻하는 슬라이더 값 
            PlayerPrefs.SetFloat("saved_SoundValue", SaveData);
            return SaveData;
        });

        return result;
    }
    public async Task<float> SoundValueLoad(float LoadData) // 그리고, 저장된 값을 불러와서 읽는다. 
    { // 값을 다시 사용하기 위해서 파라메터를 사용 
        try
        {
            // 기본적 실행 
            float result = await Task.Run(() =>
            {
                PlayerPrefs.GetFloat("saved_SoundValue");
                return LoadData;
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

        return LoadData;
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
