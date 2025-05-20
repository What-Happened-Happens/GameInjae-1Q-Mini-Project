using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : TrueFalse
{
    SpriteRenderer sr;
    Animation anim;
    float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animation>();
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrue)
        { 
            elapsedTime += Time.deltaTime; 
            if(elapsedTime > 5f)
            {
                isTrue = false;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject player = GameObject.FindWithTag("Player");
        SpriteRenderer playersr = player.GetComponent<SpriteRenderer>();
        if (sr.bounds.max.x <= playersr.bounds.min.x) // 오른쪽에 있을때,
        {
            if (isTrue)
            {
                isTrue = false;
                anim.Play("Ani_Lever_Reverse");
                Debug.Log("Lever Disabled");
            }
        }
        else if (sr.bounds.min.x >= playersr.bounds.max.x) // 왼쪽에 있을때,
        {
            if (!isTrue)
            {
                isTrue = true;
                elapsedTime = 0;
                anim.Play("Ani_Lever");
                Debug.Log("Lever Abled");
            }
        }
    }
}
