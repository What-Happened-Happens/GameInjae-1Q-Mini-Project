using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    public enum GravityState
    {
        Normal, Reversed, Weakened, Reinforced
    }
    // Start is called before the first frame update
    public GameObject button;
    GravityState currState;
    Color currentcolor;
    float gravityScale;

    public Color poweredColor;
    public Color unpoweredColor;
    public GravityState poweredState;
    public GravityState unpoweredState;
    public float reversedGravityScale;
    public float weakenedGravityScale;
    public float reinforcedGravityScale;
    public float normalGravityScale;

    Button_KangJin bk;
    bool powerOn = false;
    public bool useGravity = false;
    void Start()
    {
        currState = GravityState.Normal;
        gravityScale = normalGravityScale;
        bk = button.GetComponent<Button_KangJin>();
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
        powerOn = bk.ButtonOn;
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
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = normalGravityScale; 
    }
}
