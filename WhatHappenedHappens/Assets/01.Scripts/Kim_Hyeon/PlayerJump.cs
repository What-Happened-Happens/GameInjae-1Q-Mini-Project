using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int maxJumpCount = 2;

    private Rigidbody2D _rb2d;
    [SerializeField] private int currentJumpCount;

    public bool JumpEnabled { get; set; } = true;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        currentJumpCount = maxJumpCount;
    }

    public bool CanJump => currentJumpCount > 0 && JumpEnabled;

    public void Jump()
    {
        if (!CanJump) return;
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0);
        _rb2d.AddForce(Vector3.up * jumpForce, (ForceMode2D)ForceMode.Impulse);
        currentJumpCount--;
    }

    private void OnCollisionEnter2D(Collision2D col2d)
    {
        if (col2d.gameObject.CompareTag($"Ground"))
            currentJumpCount = maxJumpCount;
    }
}
