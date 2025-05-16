using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // State
    private IState_Player currentState;
    public enum PlayerState { Idle, Walking /*, Jumping, Hurt */ }

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Movement")]
    private float moveX = 0f;
    private float moveSpeed = 5f;
    private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    // private bool jumpPressed = false;

    [Header("Collision Size")]
    [SerializeField] private Vector2 wallBoxSize = new Vector2(0.1f, 1.0f);
    [SerializeField] private Vector2 groundBoxSize = new Vector2(0.5f, 0.07f);


    // -------------------------------------------------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // �⺻ : Idle ���� 
        ChangeState(new IdleState_Player(this));
    }


    void Update() // Ű �Է� 
    {
        currentState?.Update();

        // ���� �Է� ����
        // if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded()) jumpPressed = true;
    }


    void FixedUpdate() // ���� ���� 
    {
        // [ ���߿��� �� �浹 ]
        if (IsTouchWall() && !IsGrounded()) 
        { 
            rb.velocity = new Vector2(0, rb.velocity.y); 
        }  
        else // [ �¿� �̵� ]
        {
            Walk();
        } 

        // [ ���� ó�� ]
        /*
        if (jumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpPressed = false;
        }
        */
    }


    // ----------------------[ FSM ]---------------------------

    public void ChangeState(IState_Player newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    // [ ���¿� ���� �ִϸ��̼� �Ķ���� ���� ]
    // PlayerState : Idle, Walking, Jumping, Hurt ���ڿ��� ���ƾ� �� 
    public void SetActiveState(PlayerState activeParam)
    {
        foreach (var param in System.Enum.GetValues(typeof(PlayerState)))
        {
            animator.SetBool(param.ToString(), param.Equals(activeParam));
        }
    }

    public void Walk()
    {
        moveX = Input.GetAxisRaw("Horizontal"); // -1.0 ~ 1.0

        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // �¿� ����
        if (moveX != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(moveX);
            transform.localScale = scale;
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }


    // -----------------------[ �浹 ���� ]-------------------------


    // [ �� (=���� ���� ��) ���� ]
    public bool IsTouchWall()
    {
        Vector2 position = transform.position;
        Vector2 leftBoxCenter = position + Vector2.left * (wallBoxSize.x + 0.18f);
        Vector2 rightBoxCenter = position + Vector2.right * (wallBoxSize.x + 0.18f);
        bool hitLeftWall = Physics2D.OverlapBox(leftBoxCenter, wallBoxSize, 0f, groundLayer);
        bool hitRightWall = Physics2D.OverlapBox(rightBoxCenter, wallBoxSize, 0f, groundLayer);

        return hitLeftWall || hitRightWall;
    }

    // [ �� ���� ]
    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);
    }

    // [ physics2d �浹 �ð�ȭ ]
    private void OnDrawGizmos()
    {
        // �ٴ� üũ �ð�ȭ
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);

        // �� üũ �ð�ȭ
        Gizmos.color = IsTouchWall() ? Color.yellow : Color.blue;
        Vector2 position = transform.position;

        Vector2 leftBoxCenter = position + Vector2.left * (wallBoxSize.x + 0.18f);
        Vector2 rightBoxCenter = position + Vector2.right * (wallBoxSize.x + 0.18f);

        Gizmos.DrawWireCube(leftBoxCenter, wallBoxSize);
        Gizmos.DrawWireCube(rightBoxCenter, wallBoxSize);
    }


    // ---------------------[ Getter ]---------------------
    public Vector2 GetVelocity() { return rb.velocity; }
    public Rigidbody2D GetRigidbody() { return rb; }
}
