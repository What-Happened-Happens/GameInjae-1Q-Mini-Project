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
    public GameObject lever;
    GravityState currState;
    Color currentcolor;
    float gravityScale;

    public Color poweredColor; // Ä×À»¶§ Àû¿ë 
    public Color unpoweredColor; // ²°À»¶§ Àû¿ë
    public GravityState poweredState;
    public GravityState unpoweredState;
    public float reversedGravityScale;
    public float weakenedGravityScale;
    public float reinforcedGravityScale;
    public float normalGravityScale;

    Lever levercomponent;
    bool powerOn = false; // Ä×³ª?
    public bool useGravity = false; // ²°´Âµ¥ ÀÛµ¿ ÇÏ³ª?
    void Start()
    {
        normalGravityScale = 1;
        reversedGravityScale = -1;
        weakenedGravityScale = 0.5f;
        reinforcedGravityScale = 2;
        currState = GravityState.Normal;
        gravityScale = normalGravityScale;
        levercomponent = lever.GetComponent<Lever>();
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
        powerOn = levercomponent.leverOn;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale *= gravityScale;
        Debug.Log("Gravity Scale : " + collision.gameObject.GetComponent<Rigidbody2D>().gravityScale);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale /= gravityScale;
        Debug.Log("Gravity Scale : " + collision.gameObject.GetComponent<Rigidbody2D>().gravityScale);
    }
}
