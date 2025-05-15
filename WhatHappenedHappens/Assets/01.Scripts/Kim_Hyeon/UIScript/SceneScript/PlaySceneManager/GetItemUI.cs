using UnityEngine.UI;
using UnityEngine;

public class GetItemUI : MonoBehaviour
{
    [Header("ImagePrefab")]
    public Image _targetimagePrefab;
  
    public bool _isGetCardKey { get; set; }          // ī��Ű�� �Ծ��� �� ���� bool Ÿ�� ��ȣ 
    public bool _isGamePlaying { get; set; } = true; // ���� �÷��� ��. �ӽ÷� �׽�Ʈ�� ���� true �� ����. 

    public void SetImageColor(Image targetImage, Color color) => targetImage.color = color;
    public void SetImagePosition(Image targetImage, Vector3 position) => targetImage.transform.position = position;
    public void SetImageSprite(Image targetImage, Sprite newSprite) => targetImage.sprite = newSprite;
    public void SetImageScale(Image targetImage, Vector3 imageScale) => targetImage.rectTransform.localScale = imageScale;

    private void Start()
    {
        _targetimagePrefab = GetComponent<Image>();

        Debug.Log($"�����ϸ�, CardKeyUI �÷� ���� ���߰� ����");
        _targetimagePrefab.gameObject.SetActive(true);
        Debug.Log($"SpriteManager : �����ϸ�, �׽�Ʈ�� ���� CardKeyUI Ȱ��ȭ���� ����");

    }
 
    public void CardKeyShow(bool isStageScene, GameObject worldCardkeyObj)
    {
        Debug.Assert(isStageScene, $"CardKey ȹ����� �ʾҴµ� Show�� ȣ���߽��ϴ�. {isStageScene}");

        Vector3 worldPos = worldCardkeyObj.transform.position;
                           
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // ��ġ�� ����
        SetImagePosition(_targetimagePrefab, screenPos);

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
    private void GetCardKeyLoop(bool isStageSceneClear, GameObject worldObj)
    {
        if (isStageSceneClear) // ī��Ű�� �Ծ��� �� 
        {
            // gameObject -> WorldSpace �� ���� ������Ʈ ��ġ�� UI ��ġ�� ���� ����.
            CardKeyShow(isStageSceneClear, worldObj);
            CardKeyScale(isStageSceneClear, 5, 5); // ���� ������Ʈ ��ġ�� UI�� ��ġ���� ��, ũ�⸦ Ű���. 
           
            Vector3 imageScale = new Vector3(_targetimagePrefab.rectTransform.localScale.x, _targetimagePrefab.rectTransform.localScale.y, 0f);
            CardKeyScale(isStageSceneClear, imageScale.x, imageScale.y); // ���� ������Ʈ ��ġ�� UI�� ��ġ���� ��, ũ�⸦ Ű���. 

        }
        else if (isStageSceneClear)                             // ���������� �Ѿ�� �� �� ī��Ű�� �Ծ��� ���� ��� ������ �� ���� �ʿ�.
        {

        }
        else                                                    // ī��Ű�� ���� �ʾҰ�, ���������� �Ѿ���� �ʾ��� �� 
        {

        }
    }
}
