using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : TrueFalse
{
    SpriteRenderer sr;
    Animator animator;
    float elapsedTime;

    public ParadoxManager paradoxManager;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        elapsedTime = 0;
    }


    void Update()
    {
        if (isTrue && !paradoxManager.isRecording && !paradoxManager.isReplaying)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log("Elapsed Time: " + elapsedTime);
            if (elapsedTime > 5f)
            {
                isTrue = false;
                animator.SetTrigger("Lever_Left");
                elapsedTime = 0;
            }
        }
        // Debug.Log("Lever state: " + isTrue);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        SpriteRenderer playerSr = collision.gameObject.GetComponent<SpriteRenderer>();
        if (playerSr == null) return;

        float playerCenterX = playerSr.bounds.center.x;
        float leverCenterX = sr.bounds.center.x;

        if (playerCenterX > leverCenterX) // 플레이어가 오른쪽에 있음
        {
            if (isTrue)
            {
                animator.SetTrigger("Lever_Left");
                // Debug.Log("Lever Disabled (Right)");
                isTrue = false;
            }
        }
        else if (playerCenterX < leverCenterX) // 플레이어가 왼쪽에 있음
        {
            if (!isTrue)
            {
                animator.SetTrigger("Lever_Right");
                // Debug.Log("Lever Abled (Left)");
                elapsedTime = 0;
                isTrue = true;
            }
        }
    }
    

    public override void SetState(bool value)
    {
        if (isTrue == value) return;

        isTrue = value;
        elapsedTime = 0;

        if (animator != null)
        {
            if (value)
            {
                animator.SetTrigger("Lever_Right");
            }
            else
            {
                animator.SetTrigger("Lever_Left");
            }
        }

        Debug.Log("Lever state restored: " + value);
    }
    
}
 