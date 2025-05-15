using UnityEngine;
using UnityEngine.UI;

public class ItemPickupUIController : MonoBehaviour
{
    // ��������Ʈ ������. �������� ����� �� Ȯ���ؼ� 
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
