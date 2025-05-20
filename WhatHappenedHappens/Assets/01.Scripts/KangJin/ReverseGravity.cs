using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    public enum GravityState
    {
        Normal, Reversed, Weakened, Reinforced
    }
    // Start is called before the first frame update
    // ���� �߷���ġ ����
    public GameObject operatingObject;
    GravityState currState;
    GravityState nextState;
    Color currentcolor;
    float gravityScale;
    float originalGravityScale;

    // �۵��ϴ� �߷� ��ġ ����
    public Color poweredColor; // ������ ���� 
    public Color unpoweredColor; // ������ ����

    public GravityState poweredState;
    public GravityState unpoweredState;

    public float reversedGravityScale;
    public float weakenedGravityScale;
    public float reinforcedGravityScale;
    public float normalGravityScale;

    TrueFalse levercomponent;
    bool powerOn = false; // �׳�?
    public bool useGravity = false; // ���µ� �۵� �ϳ�?

    SpriteRenderer stepspriterenderer;
    void Start()
    {
        normalGravityScale = 1;
        reversedGravityScale = -1;
        weakenedGravityScale = 0.5f;
        reinforcedGravityScale = 2;
        /*currState = GravityState.Normal;*/
        nextState = currState;
        gravityScale = normalGravityScale;
        levercomponent = operatingObject.GetComponent<TrueFalse>();
     /*   stepspriterenderer = gravityStep.GetComponent<SpriteRenderer>();*/
    }

    // Update is called once per frame
    void Update()
    {
        CheckPower();
        ChangeState();
        gameObject.GetComponent<SpriteRenderer>().color = currentcolor;
    }

    void CheckPower()
    {
        powerOn = levercomponent.IsTrue;
        if (powerOn)
        {
            currentcolor = poweredColor;
            currState = poweredState;
        }
        else
        {
            currentcolor = unpoweredColor;
            if (useGravity)
            {
                currState = unpoweredState;
            }
            else
            {
                currState = GravityState.Normal;
            }
        }
    }

    void ChangeState()
    {
        switch (currState)
        {
            case GravityState.Normal:
                gravityScale = normalGravityScale;
                break;
            case GravityState.Reversed:
                gravityScale = reversedGravityScale;
                break;
            case GravityState.Weakened:
                gravityScale = weakenedGravityScale;
                break;
            case GravityState.Reinforced:
                gravityScale = reinforcedGravityScale;
                break;
        }
        /*nextState = currState;*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        originalGravityScale = collision.gameObject.GetComponent<Rigidbody2D>().gravityScale;
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale *= gravityScale;
        Debug.Log("Gravity Scale : " + collision.gameObject.GetComponent<Rigidbody2D>().gravityScale);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
/*        if(nextState != currState)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale *= gravityScale;
            Debug.Log("Gravity Scale : " + collision.gameObject.GetComponent<Rigidbody2D>().gravityScale);
        }
        nextState = currState;*/
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;
        Debug.Log("Gravity Scale : " + collision.gameObject.GetComponent<Rigidbody2D>().gravityScale);
    }
    public GravityState GetCurrGravityState() { return currState; }
}
