using UnityEngine.UI;
using UnityEngine;
using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System;

public class CardKeyUIcontroller : UIHelper
{
    [Header("ImagePrefab")]
    public Image _targetimagePrefab;
    public GameObject _targetCardKey;

    public bool _isGetCardKey { get; set; }          // ī��Ű�� �Ծ��� �� ���� bool Ÿ�� ��ȣ 
    public bool _isGamePlaying { get; set; } = true; // ���� �÷��� ��. �ӽ÷� �׽�Ʈ�� ���� true �� ����. 

    private void Start()
    {
        _targetimagePrefab = GetComponent<Image>();
        _targetimagePrefab.gameObject.SetActive(true);

        // test 
        _isGetCardKey = true;

        CardKeyShow(false, _targetCardKey);
        Debug.Log($"SpriteManager : �����ϸ�, �׽�Ʈ�� ���� CardKeyUI Ȱ��ȭ���� ����");
    }

    private void Update()
    {
        GetCardKeyLoop(_isGetCardKey, _targetCardKey);
    }
    public void CardKeyShow(bool isStageScene, GameObject worldCardkeyObj)
    {
        if (isStageScene && _isGamePlaying == false) return;
        Debug.Assert(isStageScene, $"CardKey ȹ����� �ʾҴµ� Show�� ȣ���߽��ϴ�. {isStageScene}");

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

        Vector2 newScale = _targetimagePrefab.rectTransform.sizeDelta; 
        SetImageScale(_targetimagePrefab.rectTransform, newScale); 

        Debug.Log($"ī��Ű�� �Ծ����ϴ�! ī��Ű ��ġ >  SpritePosition : {screenPos}");
        Debug.Log($"ī��Ű UI�� ȭ�鿡 ���Դϴ�. > isGetCardKey : {isStageScene} ");

        _targetimagePrefab.gameObject.SetActive(true);
    }

   

    // ī��Ű�� �޴� ���� false �� ��, Hide.
    public void CardKeyHide(bool isStageClearScenes)
    {
        if (isStageClearScenes == false) return;

        Debug.Log($"�������� �Ѿ���ϴ�! ī��Ű UI�� ȭ�鿡�� ����ϴ�. > isGetCardKey : {isStageClearScenes}");
        _targetimagePrefab.gameObject.SetActive(false);
    }

    // ī��Ű�� �Ծ��� �� ������ �� �ֵ��� Image ������Ʈ �߽��� ũ�� ���� �Լ� 
    public void CardKeyScale(bool isStageSceneClear, float Scalex, float Scaley)
    {
        Debug.Assert(!isStageSceneClear, $"ī��Ű�� �Ծ����ϴ�! ���� ȿ�� ���!");

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
            CardKeyScale(isStageSceneClear, 5, 5); // ���� ������Ʈ ��ġ�� UI�� ��ġ���� ��, ũ�⸦ Ű���. 

            Vector3 imageScale = new Vector3(_targetimagePrefab.rectTransform.localScale.x, _targetimagePrefab.rectTransform.localScale.y, 0f);
            CardKeyScale(isStageSceneClear, imageScale.x, imageScale.y); // ���� ������Ʈ ��ġ�� UI�� ��ġ���� ��, ũ�⸦ Ű���. 

        }
        else if (isStageSceneClear)    // ���������� �Ѿ�� �� �� ī��Ű�� �Ծ��� ���� ��� ������ �� ���� �ʿ�.
        {
            _isGetCardKey = false;
            CardKeyHide(isStageSceneClear);
            _isGamePlaying = false;
        }
        else return;
    }
}
