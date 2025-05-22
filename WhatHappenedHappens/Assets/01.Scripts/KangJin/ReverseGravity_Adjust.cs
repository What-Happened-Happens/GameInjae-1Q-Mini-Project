using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Pipeline.Tasks;
using UnityEngine;

public class ReverseGravity_Adjust : MonoBehaviour
{
    public enum GravityState
    {
        Normal, Reversed, Half, Double
    }

    // 현재 중력장치 관련
    public GameObject operatingObject;
    GravityState currState;
    GravityState prevstate;

    ParticleSystem currParticle;

    float gravityScale;
    float originalGravityScale;

    // 작동하는 중력 장치 관련
    public GravityState poweredState;
    public GravityState unpoweredState;

    public Material ReverseGravityEffect;
    public Material HalfGravityEffect;
    public Material DoubledGravityEffect;
    public Material NormalGravityEffect;

    public ParticleSystem ReverseParticlePrefab;
    public ParticleSystem HalfParticlePrefab;
    public ParticleSystem DoubledParticlePrefab;

    ParticleSystem reverseparticle;
    ParticleSystem halfparticle;
    ParticleSystem doubleparticle;

    public float reversedGravityScale;
    public float weakenedGravityScale;
    public float reinforcedGravityScale;
    public float normalGravityScale;

    TrueFalse truefalse;
    bool powerOn = false; // 켰나?
    public bool useGravity; // 껐는데 작동 하나?

    SpriteRenderer sr;
    void Start()
    {
        normalGravityScale = 1;
        reversedGravityScale = -1;
        weakenedGravityScale = 0.5f;
        reinforcedGravityScale = 2;
        currState = GravityState.Normal;
        prevstate = currState;
        gravityScale = normalGravityScale;
        truefalse = operatingObject.GetComponent<TrueFalse>();
        sr = GetComponent<SpriteRenderer>();
        /*currParticle = Instantiate(ReverseParticlePrefab, transform.position, transform.rotation);
        currParticle.Play();*/
    }

    // Update is called once per frame
    void Update()
    {
        CheckPower();
        ChangeState();
        
        if(currState != prevstate)
        {
            ChangeParticle(); 
            ChangeEffect();
            Debug.Log(currState);
            Debug.Log(prevstate);
            prevstate = currState;
        }
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
        Debug.Log("Gravity Scale : " + collision.gameObject.GetComponent<Rigidbody2D>().gravityScale);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = originalGravityScale * gravityScale;
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
                sr.material = NormalGravityEffect;
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

    void ChangeParticle()
    {
        switch (currState)
        {
            case GravityState.Normal:
                currParticle.Stop();
                Destroy(currParticle);
                break;
            case GravityState.Reversed:
                Destroy(currParticle);
                currParticle = Instantiate(ReverseParticlePrefab, transform.position, transform.rotation);
                currParticle.Play();
                break;
            case GravityState.Half:
                Destroy(currParticle);
                currParticle = Instantiate(HalfParticlePrefab, transform.position, transform.rotation);
                currParticle.Play();
                break;
            case GravityState.Double:
                Destroy(currParticle);
                currParticle = Instantiate(DoubledParticlePrefab, transform.position, transform.rotation);
                currParticle.Play();
                break;
        }
    }

    public GravityState GetCurrGravityState() { return currState; }
}
