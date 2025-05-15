using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Movement")]
    private float moveX = 0f;
    private float moveSpeed = 5f;
    private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float groundCheckRadius = 0.1f;
    private bool jumpPressed = false;

    [Header("Wall Check")]
    [SerializeField] private Vector2 wallBoxSize = new Vector2(0.1f, 1.0f);


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        // 좌우 반전
        if (moveX != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(moveX);
            transform.localScale = scale;
        }

        // 점프 입력 감지
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        // [ 공중에서 벽 충돌 ]
        if (IsTouchWall() && !IsGrounded()) 
        { 
            rb.velocity = new Vector2(0, rb.velocity.y); 
        }   
        else // [ 좌우 이동 ]
        { 
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y); 
        } 

        // [ 점프 처리 ]
        if (jumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpPressed = false;
        }

    }

    // [ 벽 (=옆에 붙은 땅) 감지 ]
    bool IsTouchWall()
    {
        Vector2 position = transform.position;
        Vector2 leftBoxCenter = position + Vector2.left * (wallBoxSize.x + 0.18f);
        Vector2 rightBoxCenter = position + Vector2.right * (wallBoxSize.x + 0.18f);
        bool hitLeftWall = Physics2D.OverlapBox(leftBoxCenter, wallBoxSize, 0f, groundLayer);
        bool hitRightWall = Physics2D.OverlapBox(rightBoxCenter, wallBoxSize, 0f, groundLayer);

        return hitLeftWall || hitRightWall;
    }

    // [ 땅 감지 ]
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // [ physics2d 충돌 시각화 ]
    private void OnDrawGizmos()
    {
        // 바닥 체크 시각화
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        // 벽 체크 시각화
        Gizmos.color = IsTouchWall() ? Color.yellow : Color.blue;
        Vector2 position = transform.position;

        Vector2 leftBoxCenter = position + Vector2.left * (wallBoxSize.x + 0.18f);
        Vector2 rightBoxCenter = position + Vector2.right * (wallBoxSize.x + 0.18f);

        Gizmos.DrawWireCube(leftBoxCenter, wallBoxSize);
        Gizmos.DrawWireCube(rightBoxCenter, wallBoxSize);
    }

}
