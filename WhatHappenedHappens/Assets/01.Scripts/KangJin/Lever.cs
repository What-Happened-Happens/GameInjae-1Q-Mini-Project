using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool leverOn = false;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(leverOn)
        {
            sr.color = Color.green;
        }
        else
        {
            sr.color = Color.red;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    /*private void OnCollisionEnter2D(Collision2D collision)*/
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (collision.gameObject == player)
        {
            Debug.Log("Collided With" + collision.gameObject.name + "!");
        }
        if (sr.bounds.max.x <= player.GetComponent<SpriteRenderer>().bounds.min.x) // 오른쪽에 있을때,
        {
            if (leverOn)
            {
                leverOn = false;
                Debug.Log("Lever Disabled");
            }
        }
        else if (sr.bounds.min.x >= player.GetComponent<SpriteRenderer>().bounds.max.x) // 왼쪽에 있을때,
        {
            if (!leverOn)
            {
                leverOn = true;
                Debug.Log("Lever Abled");
            }
        }
    }
    /*private void OnTriggerStay2D(Collider2D collision)*/
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (sr.bounds.max.x <= player.GetComponent<SpriteRenderer>().bounds.min.x) // 오른쪽에 있을때,
        {
            if (leverOn)
            {
                leverOn = false;
                Debug.Log("Lever Disabled");
            }
        }
        else if (sr.bounds.min.x >= player.GetComponent<SpriteRenderer>().bounds.max.x) // 왼쪽에 있을때,
        {
            if (!leverOn)
            {
                leverOn = true;
                Debug.Log("Lever Abled");
            }
        }
    }*/
}
