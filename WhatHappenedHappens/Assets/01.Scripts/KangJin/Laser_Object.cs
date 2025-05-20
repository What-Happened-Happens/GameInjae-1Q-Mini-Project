using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Laser_Object : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Ray2D ray2d;
    /*SpriteRenderer sr;*/
    LineRenderer lr;
    Vector3 startPos;
    Vector3 endPos;
    RaycastHit2D rayHit2D;
    int layerMask;

    /*public TrueFalse trigger;*/
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        lr = GetComponent<LineRenderer>();
        layerMask = LayerMask.GetMask("Player", "GhostPlayer","Ground");        //레이캐스트 방향 정함
        startPos = transform.position;
        Debug.Log(startPos);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        //레이캐스트 방향 정함
        rayHit2D = Physics2D.Raycast(startPos, Vector2.down * 100, 1000, layerMask);
        if (rayHit2D)
        {
            endPos = rayHit2D.point;
            Debug.Log("Start :" + startPos+"End :" + endPos);
        }
        Debug.DrawLine(startPos, rayHit2D.point, Color.magenta);
        
        lr.positionCount = 2;
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, rayHit2D.point);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().isDead = true;
        }
    }
}
