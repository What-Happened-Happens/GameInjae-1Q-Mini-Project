using UnityEngine.UI;
using UnityEngine;

// delegate test 
public delegate void Imagedelegate(Sprite _uiSprite);

public class SpriteManager : MonoBehaviour
{
    // // delegate test 
    // void SetSprite(Sprite sprite) => timerPrefab.sprite = sprite;
    //Sprite GetSprite() =>  timerPrefab.sprite;

    // void delegateTest()
    // {
    //     Imagedelegate SetImageop = SetSprite;
    //     Console.WriteLine(SetImageop);

    //     SetImageop += SetSprite;
    //     Console.WriteLine($"SetSprite : {timerPrefab.sprite.name}"); 

    //     SetImageop -= SetSprite;
    //     Console.WriteLine($"SetSprite : {timerPrefab.sprite.name}");

    // }

    [Header("ImagePrefab")]
    public Image _CardKeyImagePrefab;
    public bool _isGetCardKey { get; set; }
    
    private void Start()
    {
        Debug.Log($"시작하면, CardKeyUI 컬러 톤을 낮추고 시작");
        _CardKeyImagePrefab = GetComponent<Image>();        
        SetColor( _CardKeyImagePrefab, Color.gray);
    }

    public void SetColor(Image targetImage, Color color) => targetImage.color = color;
    public void SetImageScale(Vector2 imageScale) => _CardKeyImagePrefab.transform.localScale = imageScale;

    public void ChangedColor(Color color)
    {
        if (_isGetCardKey == false) return;

        if (_isGetCardKey == true )
        {
            SetColor(_CardKeyImagePrefab, color); 
        }
    }

   
  
}
