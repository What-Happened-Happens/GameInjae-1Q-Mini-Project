using UnityEngine;
using UnityEngine.UI;

public class ItemPickupUIController : MonoBehaviour
{
    // 스프라이트 렌더러. 아이템을 얻었는 지 확인해서 
    [Header("SpriteRenderer Script")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Image _uiImage;

    [Header("SpriteTargetObject")]
    public GameObject _target; 

    void Start()
    {
         _spriteRenderer = GetComponent<SpriteRenderer>();
        _uiImage = GetComponent<Image>(); 
    }

    void Update()
    {
        
    }

    void SpriteChange()
    {

    }
}
