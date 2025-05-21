using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : TrueFalse
{
    SpriteRenderer sr;
    Animation anim;
    float elapsedTime;

    public ParadoxManager paradoxManager;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animation>();

        elapsedTime = 0;
    }


    void Update()
    {
        if (isTrue && !paradoxManager.isRecording)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 5f)
            {
                isTrue = false;
                elapsedTime = 0;
            }
        }

        // Debug.Log("Lever state: " + isTrue);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Debug.Log("Lever Collision with Player");

        SpriteRenderer playerSr = collision.gameObject.GetComponent<SpriteRenderer>();
        if (playerSr == null) return;

        float playerCenterX = playerSr.bounds.center.x;
        float leverCenterX = sr.bounds.center.x;

        if (playerCenterX > leverCenterX) // 플레이어가 오른쪽에 있음
        {
            if (isTrue)
            {
                isTrue = false;
                anim.Play("Ani_Lever_Reverse");
                Debug.Log("Lever Disabled (Right)");
            }
        }
        else if (playerCenterX < leverCenterX) // 플레이어가 왼쪽에 있음
        {
            if (!isTrue)
            {
                isTrue = true;
                elapsedTime = 0;
                anim.Play("Ani_Lever");
                Debug.Log("Lever Abled (Left)");
            }
        }
    }
    

    public override void SetState(bool value)
    {
        if (isTrue == value) return;

        isTrue = value;
        elapsedTime = 0;

        if (anim != null)
        {
            if (value)
            {
                anim.Play("Ani_Lever");
            }
            else
            {
                anim.Play("Ani_Lever_Reverse");
            }
        }

        Debug.Log("Lever state restored: " + value);
    }
    
}
 