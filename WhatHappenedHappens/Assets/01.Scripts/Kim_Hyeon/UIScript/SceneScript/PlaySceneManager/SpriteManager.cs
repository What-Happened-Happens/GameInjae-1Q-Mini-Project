using UnityEngine.UI;
using UnityEngine;

// delegate test 
public delegate void SpriteColor(Image targetImage, Color color);         // Ÿ�� �̹����� �÷� �� ��������Ʈ 

public class SpriteManager : MonoBehaviour
{
    [Header("ImagePrefab")]
    public Image _targetimagePrefab;
    private Vector3 _offset; 
    public bool _isGetCardKey { get; set; }          // ī��Ű�� �Ծ��� �� ���� bool Ÿ�� ��ȣ 
    public bool _isGamePlaying { get; set; } = true; // ���� �÷��� ��. �ӽ÷� �׽�Ʈ�� ���� true �� ����. 

    private void Start()
    {
        _targetimagePrefab = GetComponent<Image>();

        Debug.Log($"�����ϸ�, CardKeyUI �÷� ���� ���߰� ����");        
        _targetimagePrefab.gameObject.SetActive( false );
        Debug.Log($"�����ϸ�, CardKeyUI ��Ȱ��ȭ���� ����");
    }

    // delegate ���. -> ������ �� get / set �Լ� ���� -> �Լ��� �Ϲ� ����ó�� ��� ���� 
    public void SetColor(Image targetImage, Color color) => targetImage.color = color;
    public void SetPosition(Image targetImage, Vector3 position) => targetImage.transform.position = position;
    public void SetImage(Image targetImage, Color color) => _targetimagePrefab = targetImage;
    public void SetImageScale(Vector2 imageScale) => _targetimagePrefab.transform.localScale = imageScale;

    // ������ �ڵ�
    public void GetCardKeyShow(bool isGetCardKey)
    {
        Debug.Assert(isGetCardKey, $"ī��Ű�� �Ծ����ϴ�! isGetCardKey value : {isGetCardKey}");

        GameObject cardKeyObj = _targetimagePrefab.gameObject;
        cardKeyObj.transform.position = Camera.main.ViewportToWorldPoint(_targetimagePrefab.transform.position);

        _offset = Camera.main.WorldToScreenPoint(_targetimagePrefab.transform.position);

        // ��ġ�� ����
        SetPosition(_targetimagePrefab, _offset);

        Debug.Log($"ī��Ű�� �Ծ����ϴ�! ī��Ű ��ġ >  SpritePosition : {_offset}");
        Debug.Log($"ī��Ű UI�� ȭ�鿡 ���Դϴ�. > isGetCardKey : {isGetCardKey} ");

        cardKeyObj.SetActive(true); 
    }

   

}
