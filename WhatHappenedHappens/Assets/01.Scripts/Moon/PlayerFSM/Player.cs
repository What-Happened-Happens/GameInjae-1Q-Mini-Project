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

        // 기본 : Idle 상태 
        ChangeState(new IdleState_Player(this));
    }


    void Update() // 키 입력 
    {
        currentState?.Update();

        if (isDead) ChangeState(new HurtState_Player(this));


        // [ 가속 상태 ]
        if(IsAccelerated()) SetExternalModifier(2f, 1.5f);
        else if (!IsAccelerated() && IsGrounded()) ResetExternalModifier(); // 가속 상태가 끝나고 땅을 밟았을 때 

        // 점프 입력 감지
        // if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded()) jumpPressed = true;
    }


    void FixedUpdate() // 물리 연산 
    {
        // [ 공중에서 벽 충돌 ]
        if (IsTouchWall() && !IsGrounded() && !IsAccelerated())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else // [ 좌우 이동 ]
        {
            Walk();
        }

        // [ 점프 처리 ]
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
        Debug.Log($"Changing state: {currentState?.GetType().Name} → {newState.GetType().Name}");

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    // [ 상태에 따라 애니메이션 파라미터 설정 ]
    // PlayerState : Idle, Walking, Jumping, Hurt 문자열이 같아야 함 
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

        // 좌우 반전
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


    // -----------------------[ 충돌 관련 ]-------------------------


    // [ 벽 (=옆에 붙은 땅) 감지 ]
    public bool IsTouchWall()
    {
        Vector2 position = transform.position;
        Vector2 leftBoxCenter = position + Vector2.left * (wallBoxSize.x + 0.37f);
        Vector2 rightBoxCenter = position + Vector2.right * (wallBoxSize.x + 0.37f);
        bool hitLeftWall = Physics2D.OverlapBox(leftBoxCenter, wallBoxSize, 0f, groundLayer);
        bool hitRightWall = Physics2D.OverlapBox(rightBoxCenter, wallBoxSize, 0f, groundLayer);

        return hitLeftWall || hitRightWall;
    }

    // [ 땅 감지 ]
    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);
    }

    // [ 가속 필드 감지 ]
    public bool IsAccelerated()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, AccelerateLayer);
    }

    // [ physics2d 충돌 시각화 ]
    private void OnDrawGizmos()
    {
        // 바닥 체크 시각화
        Gizmos.color = (IsGrounded()||IsAccelerated()) ? Color.green : Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);

        // 벽 체크 시각화
        Gizmos.color = IsTouchWall() ? Color.yellow : Color.blue;
        Vector2 position = transform.position;

        Vector2 leftBoxCenter = position + Vector2.left * (wallBoxSize.x + 0.37f);
        Vector2 rightBoxCenter = position + Vector2.right * (wallBoxSize.x + 0.37f);

        Gizmos.DrawWireCube(leftBoxCenter, wallBoxSize);
        Gizmos.DrawWireCube(rightBoxCenter, wallBoxSize);
    }


    // -----------------------[ 가속 관련 ]-------------------------

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
