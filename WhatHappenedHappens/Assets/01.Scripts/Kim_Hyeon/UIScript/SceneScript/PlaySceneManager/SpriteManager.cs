using UnityEngine.UI;
using UnityEngine;

// delegate test 
public delegate void SpriteColor(Image targetImage, Color color);         // 타겟 이미지의 컬러 값 딜리게이트 

public class SpriteManager : MonoBehaviour
{
    [Header("ImagePrefab")]
    public Image _targetimagePrefab;
    private Vector3 _offset; 
    public bool _isGetCardKey { get; set; }          // 카드키를 먹었을 때 받을 bool 타입 신호 
    public bool _isGamePlaying { get; set; } = true; // 게임 플레이 중. 임시로 테스트를 위해 true 로 지정. 

    private void Start()
    {
        _targetimagePrefab = GetComponent<Image>();

        Debug.Log($"시작하면, CardKeyUI 컬러 톤을 낮추고 시작");        
        _targetimagePrefab.gameObject.SetActive( false );
        Debug.Log($"시작하면, CardKeyUI 비활성화에서 시작");
    }

    // delegate 사용. -> 생성자 와 get / set 함수 소형 -> 함수를 일반 변수처럼 사용 가능 
    public void SetColor(Image targetImage, Color color) => targetImage.color = color;
    public void SetPosition(Image targetImage, Vector3 position) => targetImage.transform.position = position;
    public void SetImage(Image targetImage, Color color) => _targetimagePrefab = targetImage;
    public void SetImageScale(Vector2 imageScale) => _targetimagePrefab.transform.localScale = imageScale;

    // 수정된 코드
    public void GetCardKeyShow(bool isGetCardKey)
    {
        Debug.Assert(isGetCardKey, $"카드키를 먹었습니다! isGetCardKey value : {isGetCardKey}");

        GameObject cardKeyObj = _targetimagePrefab.gameObject;
        cardKeyObj.transform.position = Camera.main.ViewportToWorldPoint(_targetimagePrefab.transform.position);

        _offset = Camera.main.WorldToScreenPoint(_targetimagePrefab.transform.position);

        // 위치를 설정
        SetPosition(_targetimagePrefab, _offset);

        Debug.Log($"카드키를 먹었습니다! 카드키 위치 >  SpritePosition : {_offset}");
        Debug.Log($"카드키 UI가 화면에 보입니다. > isGetCardKey : {isGetCardKey} ");

        cardKeyObj.SetActive(true); 
    }

   

}
