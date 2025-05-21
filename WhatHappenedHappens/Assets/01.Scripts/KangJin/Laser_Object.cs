using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Laser_Object : MonoBehaviour
{
    Rigidbody2D rb;
    Ray2D ray2d;
    /*SpriteRenderer sr;*/
    LineRenderer lr;
    Vector3 startPos;
    Vector3 endPos;
    RaycastHit2D rayHit2D;
    int layerMask;

    [Header("Raycast Direction")]
    public bool isUp = false; // 레이캐스트 방향 정함

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        lr = GetComponent<LineRenderer>();
        layerMask = LayerMask.GetMask("Player", "GhostPlayer","Ground");        //레이캐스트 방향 정함
        startPos = transform.position;
        Debug.Log(startPos);
    }


    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        //레이캐스트 방향 정함
        if(isUp) rayHit2D = Physics2D.Raycast(startPos, Vector2.up * 100, 1000, layerMask);
        else rayHit2D = Physics2D.Raycast(startPos, Vector2.down * 100, 1000, layerMask);

        if (rayHit2D)
        {
            endPos = rayHit2D.point;
            // Debug.Log("Start :" + startPos+"End :" + endPos);

            if (rayHit2D.collider.CompareTag("Player"))
            {
                Debug.Log("Raycast hit Player!");

                // 예: Player 죽이기
                rayHit2D.collider.GetComponent<Player>().isDead = true;
            }
        }
        Debug.DrawLine(startPos, rayHit2D.point, Color.magenta);
        
        lr.positionCount = 2;
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, rayHit2D.point);
    }

}
