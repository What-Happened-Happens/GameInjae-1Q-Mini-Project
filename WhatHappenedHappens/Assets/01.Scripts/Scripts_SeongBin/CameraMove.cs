using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraMoveSpeed; // ���������� ����� ī�޶��� �����̴� �ӵ�, �������� ī�޶� �÷��̾ ������ �Ѿư�!!
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
        //Ŭ������ �̿��Ͽ� ȭ�� �̵� ������ �� ����� �ڵ�!!
        //float lx = mapSize.x - width;
        //float clampX = Mathf.Clamp(transform.position.x, center.x - lx, center.x + lx);

        //float ly = mapSize.y - height;
        //float clampY = Mathf.Clamp(transform.position.y, center.y - ly, center.y + ly);
    }
}

    
