using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool leverOn = false;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = GameObject.FindWithTag("Player");
        /*if (collision.gameObject == player)
        {
            Debug.Log("Collided With" + collision.gameObject.name + "!");
        }   */ 
        if(sr.bounds.max.x <= player.GetComponent<SpriteRenderer>().bounds.min.x) // 오른쪽에 있을때,
        {
            Debug.Log("Collided With" + collision.gameObject.name + "!");

            if (leverOn)
            {
                leverOn = false;
            }
        }
        else if (sr.bounds.min.x >= player.GetComponent<SpriteRenderer>().bounds.max.x) // 왼쪽에 있을때,
        {
            Debug.Log("Collided With" + collision.gameObject.name + "!");

            if (!leverOn)
            {
                leverOn = true;
            }
        }
    }
}
