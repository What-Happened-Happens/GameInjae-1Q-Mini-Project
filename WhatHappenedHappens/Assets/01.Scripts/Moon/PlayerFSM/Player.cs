using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("State")]
    private IState_Player currentState;
    public enum PlayerState { Idle, Walking, Jumping, Hurt }

    public bool isDead = false;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Movement")]
    private float moveX = 0f;
    [HideInInspector] public float moveSpeed = 5f;
    private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    // private bool jumpPressed = false;

    [Header("Collision Size")]
    private Vector2 wallBoxSize = new Vector2(0.1f, 1.7f);
    private Vector2 groundBoxSize = new Vector2(0.8f, 0.07f);

    [Header("External Modifier")]
    private float externalSpeedModifier = 1f;
    [SerializeField] private LayerMask AccelerateLayer;


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

        if (isDead) ChangeState(new HurtState_Player(this));


        // [ ���� ���� ]
        if(IsAccelerated()) SetExternalModifier(2f, 1.5f);
        else if (!IsAccelerated() && IsGrounded()) ResetExternalModifier(); // ���� ���°� ������ ���� ����� �� 

        // ���� �Է� ����
        // if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded()) jumpPressed = true;
    }


    void FixedUpdate() // ���� ���� 
    {
        // [ ���߿��� �� �浹 ]
        if (IsTouchWall() && !IsGrounded() && !IsAccelerated())
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
        Debug.Log($"Changing state: {currentState?.GetType().Name} �� {newState.GetType().Name}");

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

        rb.velocity = new Vector2(moveX * moveSpeed * externalSpeedModifier, rb.velocity.y);

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
        Vector2 leftBoxCenter = position + Vector2.left * (wallBoxSize.x + 0.37f);
        Vector2 rightBoxCenter = position + Vector2.right * (wallBoxSize.x + 0.37f);
        bool hitLeftWall = Physics2D.OverlapBox(leftBoxCenter, wallBoxSize, 0f, groundLayer);
        bool hitRightWall = Physics2D.OverlapBox(rightBoxCenter, wallBoxSize, 0f, groundLayer);

        return hitLeftWall || hitRightWall;
    }

    // [ �� ���� ]
    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);
    }

    // [ ���� �ʵ� ���� ]
    public bool IsAccelerated()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, AccelerateLayer);
    }

    // [ physics2d �浹 �ð�ȭ ]
    private void OnDrawGizmos()
    {
        // �ٴ� üũ �ð�ȭ
        Gizmos.color = (IsGrounded()||IsAccelerated()) ? Color.green : Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);

        // �� üũ �ð�ȭ
        Gizmos.color = IsTouchWall() ? Color.yellow : Color.blue;
        Vector2 position = transform.position;

        Vector2 leftBoxCenter = position + Vector2.left * (wallBoxSize.x + 0.37f);
        Vector2 rightBoxCenter = position + Vector2.right * (wallBoxSize.x + 0.37f);

        Gizmos.DrawWireCube(leftBoxCenter, wallBoxSize);
        Gizmos.DrawWireCube(rightBoxCenter, wallBoxSize);
    }


    // -----------------------[ ���� ���� ]-------------------------

    public void SetExternalModifier(float speedMod, float jumpMod)
    {
        externalSpeedModifier = speedMod;
    }

    public void ResetExternalModifier()
    {
        externalSpeedModifier = 1f;
    }


    // ---------------------[ Getter ]---------------------
    public Vector2 GetVelocity() { return rb.velocity; }
    public Rigidbody2D GetRigidbody() { return rb; }
}
