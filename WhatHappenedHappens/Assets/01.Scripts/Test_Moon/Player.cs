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

        // �¿� ����
        if (moveX != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(moveX);
            transform.localScale = scale;
        }

        // ���� �Է� ����
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        // [ ���߿��� �� �浹 ]
        if (IsTouchWall() && !IsGrounded()) 
        { 
            rb.velocity = new Vector2(0, rb.velocity.y); 
        }   
        else // [ �¿� �̵� ]
        { 
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y); 
        } 

        // [ ���� ó�� ]
        if (jumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpPressed = false;
        }

    }

    // [ �� (=���� ���� ��) ���� ]
    bool IsTouchWall()
    {
        Vector2 position = transform.position;
        Vector2 leftBoxCenter = position + Vector2.left * (wallBoxSize.x + 0.18f);
        Vector2 rightBoxCenter = position + Vector2.right * (wallBoxSize.x + 0.18f);
        bool hitLeftWall = Physics2D.OverlapBox(leftBoxCenter, wallBoxSize, 0f, groundLayer);
        bool hitRightWall = Physics2D.OverlapBox(rightBoxCenter, wallBoxSize, 0f, groundLayer);

        return hitLeftWall || hitRightWall;
    }

    // [ �� ���� ]
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // [ physics2d �浹 �ð�ȭ ]
    private void OnDrawGizmos()
    {
        // �ٴ� üũ �ð�ȭ
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        // �� üũ �ð�ȭ
        Gizmos.color = IsTouchWall() ? Color.yellow : Color.blue;
        Vector2 position = transform.position;

        Vector2 leftBoxCenter = position + Vector2.left * (wallBoxSize.x + 0.18f);
        Vector2 rightBoxCenter = position + Vector2.right * (wallBoxSize.x + 0.18f);

        Gizmos.DrawWireCube(leftBoxCenter, wallBoxSize);
        Gizmos.DrawWireCube(rightBoxCenter, wallBoxSize);
    }

}
