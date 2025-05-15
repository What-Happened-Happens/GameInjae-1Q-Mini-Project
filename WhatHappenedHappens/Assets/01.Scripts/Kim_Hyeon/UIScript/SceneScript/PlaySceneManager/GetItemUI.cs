using UnityEngine.UI;
using UnityEngine;

public class GetItemUI : MonoBehaviour
{
    [Header("ImagePrefab")]
    public Image _targetimagePrefab;
    private Vector3 _offset;
    public bool _isGetCardKey { get; set; }          // ī��Ű�� �Ծ��� �� ���� bool Ÿ�� ��ȣ 
    public bool _isGamePlaying { get; set; } = true; // ���� �÷��� ��. �ӽ÷� �׽�Ʈ�� ���� true �� ����. 

    // ���� ����                                              
    public void SetImageColor(Image targetImage, Color color) => targetImage.color = color;
    public void SetPosition(Image targetImage, Vector3 position) => targetImage.transform.position = position;
    public void SetImageSprite(Image targetImage, Sprite newSprite) => targetImage.sprite = newSprite;
    public void SetImageScale(Image targetImage, Vector2 imageScale) => targetImage.rectTransform.localScale = imageScale;

    private void Start()
    {
        _targetimagePrefab = GetComponent<Image>();

        Debug.Log($"�����ϸ�, CardKeyUI �÷� ���� ���߰� ����");
        _targetimagePrefab.gameObject.SetActive(true);
        Debug.Log($"SpriteManager : �����ϸ�, �׽�Ʈ�� ���� CardKeyUI Ȱ��ȭ���� ����");

    }

    public void CardKeyShow(bool isShowCardKey, GameObject worldCardkeyObj)
    {
        Debug.Assert(isShowCardKey, $"ī��Ű�� �Ծ����ϴ�! isGetCardKey value : {isShowCardKey}");

        GameObject cardKeyObj = _targetimagePrefab.gameObject;

        _offset = worldCardkeyObj.transform.position;                             // ���� �� ī��Ű ��ġ
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_offset);              // ��ũ�� �ȼ� ��ǥ�� ��ȯ

        // ��ġ�� ����
        SetPosition(_targetimagePrefab, _offset);

        Debug.Log($"ī��Ű�� �Ծ����ϴ�! ī��Ű ��ġ >  SpritePosition : {_offset}");
        Debug.Log($"ī��Ű UI�� ȭ�鿡 ���Դϴ�. > isGetCardKey : {isShowCardKey} ");

        cardKeyObj.SetActive(true);
    }

    // ī��Ű�� �޴� ���� false �� ��, Hide.
    public void CardKeyHide(bool isHideCardKey)
    {
        if (isHideCardKey == false) return;

        Debug.Log($"�������� �Ѿ���ϴ�! ī��Ű UI�� ȭ�鿡�� ����ϴ�. > isGetCardKey : {isHideCardKey}");
        _targetimagePrefab.gameObject.SetActive(false);
    }
    // ī��Ű�� �Ծ��� �� ������ �� �ֵ��� Image ������Ʈ �߽��� ũ�� ���� �Լ� 
    public void CardKeyScale(Image _targetImage, float Scalex, float Scaley)
    {
        Debug.Assert(_targetImage != null, $"ī��Ű�� �Ծ����ϴ�! ���� ȿ�� ���!");

        Vector3 imageScale = new Vector3(Scalex, Scaley, 0f);

        SetImageScale(_targetImage, imageScale);
    }
}
