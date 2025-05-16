using UnityEngine.UI;
using UnityEngine;

using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;

public class GetItemUI : UIHelper
{
    [Header("ImagePrefab")]
    public Image _targetimagePrefab;

    public bool _isGetCardKey { get; set; }          // 카드키를 먹었을 때 받을 bool 타입 신호 
    public bool _isGamePlaying { get; set; } = true; // 게임 플레이 중. 임시로 테스트를 위해 true 로 지정. 
  
    private void Start()
    {
        _targetimagePrefab = GetComponent<Image>();

        Debug.Log($"시작하면, CardKeyUI 컬러 톤을 낮추고 시작");
        _targetimagePrefab.gameObject.SetActive(true);
        Debug.Log($"SpriteManager : 시작하면, 테스트를 위해 CardKeyUI 활성화에서 시작");

    }
 
    public void CardKeyShow(bool isStageScene, GameObject worldCardkeyObj)
    {
        Debug.Assert(isStageScene, $"CardKey 획득되지 않았는데 Show를 호출했습니다. {isStageScene}");

        Vector3 worldPos = worldCardkeyObj.transform.position;
                           
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // 위치를 설정
        SetImagePosition(_targetimagePrefab, screenPos);

        Debug.Log($"카드키를 먹었습니다! 카드키 위치 >  SpritePosition : {screenPos}");
        Debug.Log($"카드키 UI가 화면에 보입니다. > isGetCardKey : {isStageScene} ");

        _targetimagePrefab.gameObject.SetActive(true);
    }

    // 카드키를 받는 값이 false 일 때, Hide.
    public void CardKeyHide(bool isStageClearScenes)
    {
        if (isStageClearScenes == false) return;

        Debug.Log($"스테이지 넘어갔습니다! 카드키 UI가 화면에서 숨깁니다. > isGetCardKey : {isStageClearScenes}");
        _targetimagePrefab.gameObject.SetActive(false);
    }

    // 카드키를 먹었을 때 연출할 수 있도록 Image 오브젝트 중심점 크기 조절 함수 
    public void CardKeyScale(bool isStageSceneClear, float Scalex, float Scaley)
    {
        Debug.Assert(!isStageSceneClear, $"카드키를 먹었습니다! 연출 효과 출력!");

        Vector3 imageScale = new Vector3(Scalex, Scaley, 0f);

        SetImageScale(_targetimagePrefab, imageScale);
    }

    // 카드키를 얻었는 지를 bool 파라메터로 받아서 로직 loop 실행    
    private void GetCardKeyLoop(bool isStageSceneClear, GameObject worldObj)
    {
        if (isStageSceneClear) // 카드키를 먹었을 때 
        {
            // gameObject -> WorldSpace 의 게임 오브젝트 위치에 UI 위치를 띄우기 위함.
            CardKeyShow(isStageSceneClear, worldObj);
            CardKeyScale(isStageSceneClear, 5, 5); // 게임 오브젝트 위치에 UI가 위치했을 때, 크기를 키운다. 
           
            Vector3 imageScale = new Vector3(_targetimagePrefab.rectTransform.localScale.x, _targetimagePrefab.rectTransform.localScale.y, 0f);
            CardKeyScale(isStageSceneClear, imageScale.x, imageScale.y); // 게임 오브젝트 위치에 UI가 위치했을 때, 크기를 키운다. 

        }
        else if (isStageSceneClear)    // 스테이지를 넘어갔을 때 와 카드키를 먹었을 때를 어떻게 구분할 지 결정 필요.
        {

        }
        else                          // 카드키를 먹지 않았고, 스테이지를 넘어가지도 않았을 때 
        {

        }
    }
}
