using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MoveOverTime : MonoBehaviour
{
    public GameObject isGroundCheck;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;


    private Rigidbody2D rb;
    private bool isGrounded;
    bool isSkillStart;
    float inSkillTime;
    Vector3 fitstSkillPos;
    GameObject me;

    void Start()
    {
        fitstSkillPos = new Vector3(0,0,0);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        SkillTime();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // 좌우 키 입력
        Vector3 velocity = rb.velocity;
        velocity.x = moveHorizontal * moveSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, 0);
    }

    void Jump()
    {
        isGrounded = isGroundCheck.GetComponent<player_Groundcheck>().isGroundCheck;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("점프!!");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }
        Debug.Log(isGrounded);
    }

    void SkillTime()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(isSkillStart == false) //  R이 눌린 시점1번만 위치를 입력함!!
            {
                fitstSkillPos = transform.position;
            }
            isSkillStart = true;
            inSkillTime = 0;
        }

        if (isSkillStart)
        {
            inSkillTime += Time.deltaTime;
        }

        if (isSkillStart && inSkillTime > 5)
        {
            isSkillStart = false;
            inSkillTime = 0;
            transform.position = fitstSkillPos;
        }
    }
}
