using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraMoveSpeed; // 선형보간에 사용할 카메라의 움직이는 속도, 빠를수록 카메라가 플레이어를 빠르게 쫓아감!!
    Transform playerTrans;


    // Start is called before the first frame update
    void Start()
    {
        cameraMoveSpeed = 5f;
        playerTrans = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        CameraTrans();
    }

    void CameraTrans()
    {
        transform.position = Vector2.Lerp(transform.position, playerTrans.position, cameraMoveSpeed * Time.deltaTime);
        transform.Translate(0, 0, -10f);
        //클램프를 이용하여 화면 이동 제한할 때 사용할 코드!!
        //float lx = mapSize.x - width;
        //float clampX = Mathf.Clamp(transform.position.x, center.x - lx, center.x + lx);

        //float ly = mapSize.y - height;
        //float clampY = Mathf.Clamp(transform.position.y, center.y - ly, center.y + ly);
    }
}

    
