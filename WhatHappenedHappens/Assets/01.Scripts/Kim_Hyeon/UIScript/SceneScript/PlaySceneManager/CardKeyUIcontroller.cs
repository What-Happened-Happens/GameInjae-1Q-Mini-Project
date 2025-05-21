using UnityEngine.UI;
using UnityEngine;
using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;


public class CardKeyUIcontroller : UIHelper
{
    [Header("ImagePrefab")]
    public Image _targetimagePrefab;
    public GameObject _targetCardKey;

    public bool _isGetCardKey { get; set; } = true;        // ī��Ű�� �Ծ��� �� ���� bool Ÿ�� ��ȣ 
    public bool _isGamePlaying { get; set; } = true; // ���� �÷��� ��. �ӽ÷� �׽�Ʈ�� ���� true �� ����. 

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
        Debug.Log($"SpriteManager : �����ϸ�, �׽�Ʈ�� ���� CardKeyUI Ȱ��ȭ���� ����");
      
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
        // ��ġ�� ����
        SetImageCanvasPosition(_targetimagePrefab.rectTransform.anchoredPosition, screenPos);

        Vector2 newScale = new Vector2(1, 1);
     
        Debug.Log($"ī��Ű�� �Ծ����ϴ�! ī��Ű ��ġ >  SpritePosition : {screenPos}");
        Debug.Log($"ī��Ű UI�� ȭ�鿡 ���Դϴ�. > isGetCardKey : {isStageclear} ");

        _targetimagePrefab.gameObject.SetActive(true);
        SetImageColor(_targetimagePrefab, Color.white); 
    }   

    // ī��Ű�� �޴� ���� false �� ��, Hide.
    public void CardKeyHide(bool isStageClearScenes)
    {
        if (isStageClearScenes != false && _isHide == false) return;
        _isShow = false; 
     
        Debug.Log($"�������� �Ѿ���ϴ�! ī��Ű UI�� ȭ�鿡�� ����ϴ�. > isGetCardKey : {isStageClearScenes}");
        _targetimagePrefab.gameObject.SetActive(false);
    }

    // ī��Ű�� �Ծ��� �� ������ �� �ֵ��� Image ������Ʈ �߽��� ũ�� ���� �Լ� 
    public void CardKeyScale(bool isStageSceneClear, float Scalex, float Scaley)
    {
        Debug.Log($"ī��Ű�� �Ծ����ϴ�! ���� ȿ�� ���!");

        Vector3 imageScale = new Vector3(Scalex, Scaley, 0f);

        SetImageScale(_targetimagePrefab, imageScale);
    }

    // ī��Ű�� ����� ���� bool �Ķ���ͷ� �޾Ƽ� ���� loop ����    
    public void GetCardKeyLoop(bool isStageSceneClear, GameObject worldObj)
    {
        if (isStageSceneClear) // ī��Ű�� �Ծ��� �� 
        {
            if (!_isGetCardKey || !_isGamePlaying) return;

            // gameObject -> WorldSpace �� ���� ������Ʈ ��ġ�� UI ��ġ�� ���� ����.
            CardKeyShow(isStageSceneClear, worldObj);
            CardKeyScale(isStageSceneClear, 1, 1); // ���� ������Ʈ ��ġ�� UI�� ��ġ���� ��, ũ�⸦ Ű���. 

            Vector3 imageStartScale = new Vector3(1f, 1f, 0f);
            CardKeyScale(isStageSceneClear, imageStartScale.x, imageStartScale.y); // ���� ������Ʈ ��ġ�� UI�� ��ġ���� ��, ũ�⸦ Ű���.

            Vector3 imageMiddleScale = new Vector3(2f, 2f, 0f);
            CardKeyScale(isStageSceneClear, imageMiddleScale.x, imageMiddleScale.y); // ���� ������Ʈ ��ġ�� UI�� ��ġ���� ��, ũ�⸦ Ű���.

            Vector3 imageEndScale = new Vector3(1f, 1f, 0f);
            CardKeyScale(isStageSceneClear, imageEndScale.x , imageEndScale.y); // ���� ������Ʈ ��ġ�� UI�� ��ġ���� ��, ũ�⸦ Ű���. 

        }
        else if (!isStageSceneClear)    // ���������� �Ѿ�� �� �� ī��Ű�� �Ծ��� ���� ��� ������ �� ���� �ʿ�.
        {
            _isGetCardKey = false;
            CardKeyHide(isStageSceneClear);
            _isGamePlaying = false;
        }
        else return;
    }
}
