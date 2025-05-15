using UnityEngine.UI;
using UnityEngine;

public class GetItemUI : MonoBehaviour
{
    [Header("ImagePrefab")]
    public Image _targetimagePrefab;
    private Vector3 _offset;
    public bool _isGetCardKey { get; set; }          // 카드키를 먹었을 때 받을 bool 타입 신호 
    public bool _isGamePlaying { get; set; } = true; // 게임 플레이 중. 임시로 테스트를 위해 true 로 지정. 

    // 람다 형식                                              
    public void SetImageColor(Image targetImage, Color color) => targetImage.color = color;
    public void SetPosition(Image targetImage, Vector3 position) => targetImage.transform.position = position;
    public void SetImageSprite(Image targetImage, Sprite newSprite) => targetImage.sprite = newSprite;
    public void SetImageScale(Image targetImage, Vector2 imageScale) => targetImage.rectTransform.localScale = imageScale;

    private void Start()
    {
        _targetimagePrefab = GetComponent<Image>();

        Debug.Log($"시작하면, CardKeyUI 컬러 톤을 낮추고 시작");
        _targetimagePrefab.gameObject.SetActive(true);
        Debug.Log($"SpriteManager : 시작하면, 테스트를 위해 CardKeyUI 활성화에서 시작");

    }

    public void CardKeyShow(bool isShowCardKey, GameObject worldCardkeyObj)
    {
        Debug.Assert(isShowCardKey, $"카드키를 먹었습니다! isGetCardKey value : {isShowCardKey}");

        GameObject cardKeyObj = _targetimagePrefab.gameObject;

        _offset = worldCardkeyObj.transform.position;                             // 월드 상 카드키 위치
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_offset);              // 스크린 픽셀 좌표로 변환

        // 위치를 설정
        SetPosition(_targetimagePrefab, _offset);

        Debug.Log($"카드키를 먹었습니다! 카드키 위치 >  SpritePosition : {_offset}");
        Debug.Log($"카드키 UI가 화면에 보입니다. > isGetCardKey : {isShowCardKey} ");

        cardKeyObj.SetActive(true);
    }

    // 카드키를 받는 값이 false 일 때, Hide.
    public void CardKeyHide(bool isHideCardKey)
    {
        if (isHideCardKey == false) return;

        Debug.Log($"스테이지 넘어갔습니다! 카드키 UI가 화면에서 숨깁니다. > isGetCardKey : {isHideCardKey}");
        _targetimagePrefab.gameObject.SetActive(false);
    }
    // 카드키를 먹었을 때 연출할 수 있도록 Image 오브젝트 중심점 크기 조절 함수 
    public void CardKeyScale(Image _targetImage, float Scalex, float Scaley)
    {
        Debug.Assert(_targetImage != null, $"카드키를 먹었습니다! 연출 효과 출력!");

        Vector3 imageScale = new Vector3(Scalex, Scaley, 0f);

        SetImageScale(_targetImage, imageScale);
    }
}
