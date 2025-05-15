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
        if (sr.bounds.max.x <= player.GetComponent<SpriteRenderer>().bounds.min.x) // 오른쪽에 있을때,
        {
            if (isTrue)
            {
                isTrue = false;
                Debug.Log("Lever Disabled");
            }
        }
        else if (sr.bounds.min.x >= player.GetComponent<SpriteRenderer>().bounds.max.x) // 왼쪽에 있을때,
        {
            if (!isTrue)
            {
                isTrue = true;
                Debug.Log("Lever Abled");
            }
        }
    }
}
