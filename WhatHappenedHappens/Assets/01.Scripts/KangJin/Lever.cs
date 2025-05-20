using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : TrueFalse
{
    SpriteRenderer sr;
    Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
/*        if(isTrue)
        {
            *//*sr.color = Color.green;*//*
        }
        else
        {
            *//*sr.color = Color.red;*//*
        }*/
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
                anim.Play("Ani_Lever");
                Debug.Log("Lever Abled");
            }
        }
    }
}
