using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Item_Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotateSpeed;
    public float rotateTime;
    bool isEntered = false;
    float elapsedTime;
    Vector3 cameraPos;
    Vector3 directionVector;
    void Start()
    {
        /*rotateSpeed = 10;
        rotateTime = 1f;*/
        elapsedTime = 0;
        cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.9f));
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.position = cameraPos;*/
        if (isEntered)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime <= rotateTime)
            {
                transform.rotation *= Quaternion.Euler(0, rotateSpeed * (rotateTime - elapsedTime), 0);
                Debug.Log("pitch");
            }
            else if (elapsedTime > rotateTime)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                directionVector = (cameraPos - gameObject.transform.position).normalized;
                Debug.Log("cameraPos : " + cameraPos);
                Debug.Log("directionVector : " + directionVector);
                /*Debug.Log("position : " + gameObject.transform.position);*/
                gameObject.transform.position += new Vector3(directionVector.x * Time.deltaTime * 10, directionVector.y * Time.deltaTime * 10);
                Debug.Log("position : " + gameObject.transform.position);

                if (elapsedTime > rotateTime * 2)
                {
                    if (directionVector.x<0.01f && directionVector.y <0.01f)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isEntered = true;
    }
}
