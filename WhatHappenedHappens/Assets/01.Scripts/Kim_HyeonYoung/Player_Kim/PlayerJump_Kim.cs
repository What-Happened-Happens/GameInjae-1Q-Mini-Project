using UnityEngine;

public class PlayerJump_Kim : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int maxJumpCount = 2; // Maxinum Jump Count 
    
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private int currentJumpCount;
    public bool JumpEnabled {  get; set; } = true;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        currentJumpCount = maxJumpCount;
    }

    public bool CanJump => currentJumpCount > 0 && JumpEnabled; 

    public void Jump()
    {
        Debug.Log($"You can DoubleJumping! JumpCount : {currentJumpCount}"); 
        if (!CanJump) return;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        _rigidbody2D.AddForce(Vector3.up * jumpForce, (ForceMode2D) ForceMode.Impulse);
        currentJumpCount--;
    }

    private void OnCollisionEnter2D(Collision2D col2d)
    {
        if (col2d.gameObject.CompareTag("Well"))
            currentJumpCount = maxJumpCount;
    }

}
