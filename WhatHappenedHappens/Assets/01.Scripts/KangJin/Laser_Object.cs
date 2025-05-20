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
    RaycastHit2D rayHit2D;

    /*public TrueFalse trigger;*/
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        lr = GetComponent<LineRenderer>();
        /*sr = GetComponent<SpriteRenderer>();
        sr.spriteSortPoint = SpriteSortPoint.Pivot;*/

        //레이캐스트 방향 정함
        startPos = gameObject.transform.position;
        Debug.Log(startPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /*if(trigger.IsTrue)*/
        //레이캐스트 방향 정함
        {
            rayHit2D = Physics2D.Raycast(startPos, Vector2.down*100);
            Debug.DrawLine(startPos, rayHit2D.point, Color.magenta);
            float posToPointY = startPos.y - rayHit2D.point.y;
            float fixedPosY = startPos.y - (posToPointY/2);
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, rayHit2D.distance, gameObject.transform.localScale.z);
        }
    }

/*    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name+" colliding!");
    }*/
}
