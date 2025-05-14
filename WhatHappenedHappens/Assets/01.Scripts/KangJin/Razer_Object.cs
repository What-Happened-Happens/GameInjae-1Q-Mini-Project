using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Razer_Object : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    /*RaycastHit2D rayHit2D;*/
    Ray2D ray2d;
    SpriteRenderer sr;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        sr = GetComponent<SpriteRenderer>();
        
        //레이캐스트 방향 정함
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //레이캐스트 방향 정함
        RaycastHit2D rayHit2D = Physics2D.Raycast(transform.position, Vector2.down*100);
        Debug.DrawLine(transform.position, rayHit2D.point, Color.magenta);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, rayHit2D.distance, gameObject.transform.localScale.z);
        float halfRay = rayHit2D.distance/2;
        float rayMiddleYPos = gameObject.transform.position.y - halfRay;
        
        /*gameObject.transform.position = new Vector3(transform.position.x, raymiddle);*/

        if (rayHit2D)
        {
            // 거리 설정
            float distance = Mathf.Abs(rayHit2D.point.y-transform.position.y);
            Debug.Log(distance);
            Debug.Log("point : " + rayHit2D.collider.name);
        }
        else
        {
            Debug.Log("no hit");
        }


    }
}
