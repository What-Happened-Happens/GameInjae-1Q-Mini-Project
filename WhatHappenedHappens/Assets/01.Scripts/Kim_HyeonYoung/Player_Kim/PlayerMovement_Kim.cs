using UnityEngine;

public class PlayerMovement_Kim : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D  _rb2D;
    private SpriteRenderer _spriteRenderer; 
    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
  
    public void Move(float direction)  {
        _rb2D.velocity = new Vector2(direction * moveSpeed, _rb2D.velocity.y); 
        IsMoving = Mathf.Abs(direction) > 0.1f;
        if (direction != 0)
        {
            _spriteRenderer.flipX = direction < 0;
        } 
    }
}
