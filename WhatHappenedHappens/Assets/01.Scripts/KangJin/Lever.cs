using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : TrueFalse
{
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrue)
        {
            sr.color = Color.green;
        }
        else
        {
            sr.color = Color.red;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject player = GameObject.FindWithTag("Player");
        SpriteRenderer playersr = player.GetComponent<SpriteRenderer>();
        if (sr.bounds.max.x <= playersr.bounds.min.x) // �����ʿ� ������,
        {
            if (isTrue)
            {
                isTrue = false;
                Debug.Log("Lever Disabled");
            }
        }
        else if (sr.bounds.min.x >= playersr.bounds.max.x) // ���ʿ� ������,
        {
            if (!isTrue)
            {
                isTrue = true;
                Debug.Log("Lever Abled");
            }
        }
    }
}
