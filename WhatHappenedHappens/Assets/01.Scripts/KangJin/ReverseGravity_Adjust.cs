using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReverseGravity_Adjust : MonoBehaviour
{
    public enum GravityState
    {
        Normal, Reversed, Half, Double
    }
    // Start is called before the first frame update
    // ���� �߷���ġ ����
    public GameObject operatingObject;
    GravityState currState;
    float gravityScale;
    float originalGravityScale;

    // �۵��ϴ� �߷� ��ġ ����
    /*public Color poweredColor; // ������ ���� 
    public Color unpoweredColor; // ������ ����*/
    
    // ���� ��� ��Ƽ����� ��ü
    public Material ReverseGravityEffect;
    public Material HalfGravityEffect;
    public Material DoubledGravityEffect;

    public GravityState poweredState;
    public GravityState unpoweredState;

    public float reversedGravityScale;
    public float weakenedGravityScale;
    public float reinforcedGravityScale;
    public float normalGravityScale;

    TrueFalse truefalse;
    bool powerOn = false; // �׳�?
    public bool useGravity = false; // ���µ� �۵� �ϳ�?

    SpriteRenderer sr;
    void Start()
    {
        normalGravityScale = 1;
        reversedGravityScale = -1;
        weakenedGravityScale = 0.5f;
        reinforcedGravityScale = 2;
        currState = GravityState.Normal;
        gravityScale = normalGravityScale;
        truefalse = operatingObject.GetComponent<TrueFalse>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPower();
        ChangeState();
        ChangeEffect();
    }

    void CheckPower()
    {
        powerOn = truefalse.IsTrue;
        if (powerOn)
        {
            currState = poweredState;
        }
        else
        {
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

    public void ChangeState()
    {
        switch (currState)
        {
            case GravityState.Normal:
                gravityScale = normalGravityScale;
                break;
            case GravityState.Reversed:
                gravityScale = reversedGravityScale;
                break;
            case GravityState.Half:
                gravityScale = weakenedGravityScale;
                break;
            case GravityState.Double:
                gravityScale = reinforcedGravityScale;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        originalGravityScale = collision.gameObject.GetComponent<Rigidbody2D>().gravityScale;
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale *= gravityScale;
        Debug.Log("Gravity Scale : " + collision.gameObject.GetComponent<Rigidbody2D>().gravityScale);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;
        Debug.Log("Gravity Scale : " + collision.gameObject.GetComponent<Rigidbody2D>().gravityScale);
    }

    public void ChangeEffect()
    {
        switch (currState)
        {
            case GravityState.Normal:
                sr.material = null;
                break;
            case GravityState.Reversed:
                sr.material = ReverseGravityEffect;
                break;
            case GravityState.Half:
                sr.material = HalfGravityEffect;
                break;
            case GravityState.Double:
                sr.material = DoubledGravityEffect;
                break;
        }
    }
    public GravityState GetCurrGravityState() { return currState; }
}
