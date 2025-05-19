using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Laser_Object : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Ray2D ray2d;
    SpriteRenderer sr;
    Vector3 startPos;
    RaycastHit2D rayHit2D;
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        sr = GetComponent<SpriteRenderer>();
        sr.spriteSortPoint = SpriteSortPoint.Pivot;
        //레이캐스트 방향 정함
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        //레이캐스트 방향 정함
        rayHit2D = Physics2D.Raycast(startPos, Vector2.down*100);
        Debug.DrawLine(startPos, rayHit2D.point, Color.magenta);
        float posToPointY = startPos.y - rayHit2D.point.y;
        float fixedPosY = startPos.y - (posToPointY/2);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, rayHit2D.distance, gameObject.transform.localScale.z);
    }
}
