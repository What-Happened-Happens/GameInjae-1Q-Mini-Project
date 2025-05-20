using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;


public class SpikeWheel : MonoBehaviour
{
    public float wheelTurnSpeed; // 1초당 바퀴가 도는 횟수
    Vector3 startPoint; //  자식 오브젝트의 경로중 제일 처음에 들어있는 포인트

    void Start()
    {
        wheelTurnSpeed = 360f;
    }

    void Update()
    {
        SpinWheel();     // 바퀴를 돌게하는 함수
    }
    private void FixedUpdate()
    {
        

    }
    void SpinWheel()
    {
        transform.Rotate(new Vector3(0, 0, -360f), Time.deltaTime * wheelTurnSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 상호작용 넣을 곳
        if (collision.tag=="Player")
        {
            collision.gameObject.GetComponent<Player>().isDead = true;
        }
    }
}
