using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;


// 사용법 : 움직이지 않는 톱니를 생각하면 그냥 배치
//          움직이는 톱니를 생각하면, WayPoint의 자식 오브젝트를 넣으면 , 그 자식오브젝트의  Position을 순서대로 따라가고
//          끝까지 갔을 경우 돌아온다.
public class SpikeWheel : MonoBehaviour
{
    public GameObject wayPoint; // 부모 오브젝트의 경로를 받을 오브젝트
    List<Transform> chilWayPoints = new List<Transform>();      //  자식 오브젝트의 경로들의 위치를 받아올 리스트
    public float wheelTurnSpeed; // 1초당 바퀴가 도는 횟수
    Vector3 startPoint; //  자식 오브젝트의 경로중 제일 처음에 들어있는 포인트
    Vector3 endPoint;   //                  ''          마지막에 들어있는 포인트                 
    int curWayPoint;    // 현재 오브젝트가 갈려는 포인트의 인덱스
    float elapsedTime;  //  구간을 진행할
    Transform initPos;
    float moveTime;     
    float moveDisPer;
    bool isReturning;  //end waypoint에서 돌아갈때 사용

    private AudioSource audioSource;

    void Start()
    {
        moveTime = 2f;
        moveDisPer = 0f;
        isReturning = false;
        elapsedTime = 0f;
        curWayPoint = 1;
        wheelTurnSpeed = 360f;
        initPos = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        if (wayPoint != null && wayPoint.transform.childCount >= 2)
        {
            chilWayPoints.Clear(); // 혹시 Start 여러 번 돌 수 있으니 초기화
            foreach (Transform t in wayPoint.transform)
                chilWayPoints.Add(t);

            startPoint = chilWayPoints[0].position;
            endPoint = chilWayPoints[1].position;
        }
        else
        {
            // 아무 것도 안 하고 현재 위치 유지 (또는 원하는 기본 위치)
            startPoint = transform.position;
            endPoint = transform.position;
        }
        transform.position = startPoint;
    }

    void Update()
    {
        SpinWheel();     // 바퀴를 돌게하는 함수
        SetNextTarget(); // 위치을 선택하는 함수 
        MoveWayPoint();  // 톱니를 움직이게하는 함수
    }
    private void FixedUpdate()
    {
        

    }
    void SpinWheel()
    {
        transform.Rotate(new Vector3(0, 0, -360f), Time.deltaTime * wheelTurnSpeed);
    }



    void MoveWayPoint()
    {
        if (wayPoint == null) { return; }
        elapsedTime += Time.deltaTime;
        moveDisPer = Mathf.Clamp01(-Mathf.Cos(elapsedTime * 360 * Mathf.Deg2Rad)/2f);
        transform.position = Vector3.Lerp(startPoint, endPoint, moveDisPer);
    }

    void SetNextTarget()
    {
        Debug.Log("Time : "+ elapsedTime);
        if (wayPoint == null) { return; }
        if (moveDisPer >= 1f)
        {
            transform.position = endPoint;
            if (isReturning == false)
                curWayPoint++;
            else
                curWayPoint--;
        }

        if (curWayPoint < 0)
        { 
            isReturning = false;
            Debug.Log("0 보다 작음 : " + curWayPoint);
            curWayPoint = 1;
        }
        else if (curWayPoint > chilWayPoints.Count - 1)
        {
            isReturning = true;
            curWayPoint = chilWayPoints.Count - 2;
            Debug.Log("0 보다 큼!");
        }
        else
            Debug.Log("웨이포인트 범위 안!!");

        if (moveDisPer >= 1f)
        {
            startPoint = transform.position;
            endPoint = chilWayPoints[curWayPoint].position;
            moveDisPer = 0f;
            elapsedTime = 0f;
        }
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
