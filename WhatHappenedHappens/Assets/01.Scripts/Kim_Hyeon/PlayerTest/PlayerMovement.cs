using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    private Rigidbody2D _rd2d;
    private SpriteRenderer _spriteRenderer;
    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _rd2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(float direction)
    {
        _rd2d.velocity = new Vector2(direction * moveSpeed, _rd2d.velocity.y);
        IsMoving = Mathf.Abs(direction) > 0;

        if (direction != 0)
            _spriteRenderer.flipX = direction < 0;
    }
}
