using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class SpikeWheel : MonoBehaviour
{
    public GameObject wayPoint; // �θ� ������Ʈ�� ��θ� ���� ������Ʈ
    List<Transform> chilWayPoints = new List<Transform>();      //  �ڽ� ������Ʈ�� ��ε��� ��ġ�� �޾ƿ� ����Ʈ
    public float wheelTurnSpeed; // 1�ʴ� ������ ���� Ƚ��
    Vector3 startPoint; //  �ڽ� ������Ʈ�� ����� ���� ó���� ����ִ� ����Ʈ
    Vector3 endPoint;   //                  ''          �������� ����ִ� ����Ʈ                 
    int curWayPoint;    // ���� ������Ʈ�� ������ ����Ʈ�� �ε���
    float elapsedTime;  //  ������ ������
    float moveTime;
    float moveDisPer;
    bool isReturning;  //end waypoint���� ���ư��� ���
    void Start()
    {
        moveTime = 2f;
        moveDisPer = 0f;
        isReturning = false;
        elapsedTime = 0f;
        curWayPoint = 0;
        wheelTurnSpeed = 360f;
        if (wayPoint != null )
        {
            foreach (Transform t in wayPoint.transform)
                chilWayPoints.Add(t);
            startPoint = chilWayPoints[0].position;
            endPoint = chilWayPoints[1].position;
        }
        transform.position = startPoint;
    }

    void Update()
    {
        SpinWheel();
        SetNextTarget();
        MoveWayPoint();
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
        moveDisPer = Mathf.Clamp01(elapsedTime / moveTime);
        transform.position = Vector3.Lerp(startPoint, endPoint, moveDisPer);
    }

    void SetNextTarget()
    {
        if (wayPoint == null) { return; }
        if (moveDisPer >= 0.95f)
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
            Debug.Log("0 ���� ���� : " + curWayPoint);
            curWayPoint = 1;
        }
        else if (curWayPoint > chilWayPoints.Count - 1)
        {
            Debug.Log("ī��Ʈ : " + chilWayPoints.Count);
            isReturning = true;
            curWayPoint = chilWayPoints.Count - 2;
            Debug.Log("0 ���� ŭ!");
        }
        else
            Debug.Log("��������Ʈ ���� ��!!");

        if (moveDisPer >= 0.95f)
        {
            startPoint = transform.position;
            endPoint = chilWayPoints[curWayPoint].position;
            moveDisPer = 0f;
            elapsedTime = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� ��ȣ�ۿ� ���� ��
        if (collision.tag=="Player")
        {

        }
    }
}
