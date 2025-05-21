using UnityEngine.UI;
using UnityEngine;
using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;


public class CardKeyUIcontroller : UIHelper
{
    [Header("ImagePrefab")]
    public Image _targetimagePrefab;
    public GameObject _targetCardKey;

    public bool _isGetCardKey { get; set; } = true;        // 카드키를 먹었을 때 받을 bool 타입 신호 
    public bool _isGamePlaying { get; set; } = true; // 게임 플레이 중. 임시로 테스트를 위해 true 로 지정. 

    public bool _isShow {  get; set; } = true; 
    public bool _isHide { get; set; } = true; 

    private void Start()
    {
        _targetimagePrefab = GetComponent<Image>();
        _targetimagePrefab.gameObject.SetActive(true);

        // test 
        _isGetCardKey = false;
        SetImageColor(_targetimagePrefab, Color.gray);
        CardKeyShow(_isGetCardKey, _targetCardKey);
        Debug.Log($"SpriteManager : 시작하면, 테스트를 위해 CardKeyUI 활성화에서 시작");
      
    }

    private void Update()
    {
        GetCardKeyLoop(_isGetCardKey, _targetCardKey);
    }
    public void CardKeyShow(bool isStageclear, GameObject worldCardkeyObj)
    {
        if (isStageclear && _isGamePlaying == false && _isShow == false) return;

        _isHide = false;
        _isShow = true; 
        Vector3 worldPos = worldCardkeyObj.transform.position;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        var canvas = _targetimagePrefab.canvas;
        var canvasRt = canvas.transform as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        canvasRt,
        screenPos,
        canvas.renderMode == RenderMode.ScreenSpaceOverlay
         ? null
         : Camera.main,
         out Vector2 localPoint
 );
        var d = _targetimagePrefab.rectTransform.anchoredPosition;
        // 위치를 설정
        SetImageCanvasPosition(_targetimagePrefab.rectTransform.anchoredPosition, screenPos);

        Vector2 newScale = new Vector2(1, 1);
     
        Debug.Log($"카드키를 먹었습니다! 카드키 위치 >  SpritePosition : {screenPos}");
        Debug.Log($"카드키 UI가 화면에 보입니다. > isGetCardKey : {isStageclear} ");

        _targetimagePrefab.gameObject.SetActive(true);
        SetImageColor(_targetimagePrefab, Color.white); 
    }   

    // 카드키를 받는 값이 false 일 때, Hide.
    public void CardKeyHide(bool isStageClearScenes)
    {
        if (isStageClearScenes != false && _isHide == false) return;
        _isShow = false; 
     
        Debug.Log($"스테이지 넘어갔습니다! 카드키 UI가 화면에서 숨깁니다. > isGetCardKey : {isStageClearScenes}");
        _targetimagePrefab.gameObject.SetActive(false);
    }

    // 카드키를 먹었을 때 연출할 수 있도록 Image 오브젝트 중심점 크기 조절 함수 
    public void CardKeyScale(bool isStageSceneClear, float Scalex, float Scaley)
    {
        Debug.Log($"카드키를 먹었습니다! 연출 효과 출력!");

        Vector3 imageScale = new Vector3(Scalex, Scaley, 0f);

        SetImageScale(_targetimagePrefab, imageScale);
    }

    // 카드키를 얻었는 지를 bool 파라메터로 받아서 로직 loop 실행    
    public void GetCardKeyLoop(bool isStageSceneClear, GameObject worldObj)
    {
        if (isStageSceneClear) // 카드키를 먹었을 때 
        {
            if (!_isGetCardKey || !_isGamePlaying) return;

            // gameObject -> WorldSpace 의 게임 오브젝트 위치에 UI 위치를 띄우기 위함.
            CardKeyShow(isStageSceneClear, worldObj);
            CardKeyScale(isStageSceneClear, 1, 1); // 게임 오브젝트 위치에 UI가 위치했을 때, 크기를 키운다. 

            Vector3 imageStartScale = new Vector3(1f, 1f, 0f);
            CardKeyScale(isStageSceneClear, imageStartScale.x, imageStartScale.y); // 게임 오브젝트 위치에 UI가 위치했을 때, 크기를 키운다.

            Vector3 imageMiddleScale = new Vector3(2f, 2f, 0f);
            CardKeyScale(isStageSceneClear, imageMiddleScale.x, imageMiddleScale.y); // 게임 오브젝트 위치에 UI가 위치했을 때, 크기를 키운다.

            Vector3 imageEndScale = new Vector3(1f, 1f, 0f);
            CardKeyScale(isStageSceneClear, imageEndScale.x , imageEndScale.y); // 게임 오브젝트 위치에 UI가 위치했을 때, 크기를 키운다. 

        }
        else if (!isStageSceneClear)    // 스테이지를 넘어갔을 때 와 카드키를 먹었을 때를 어떻게 구분할 지 결정 필요.
        {
            _isGetCardKey = false;
            CardKeyHide(isStageSceneClear);
            _isGamePlaying = false;
        }
        else return;
    }
}
