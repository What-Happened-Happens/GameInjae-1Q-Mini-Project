using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;


// ���� : �������� �ʴ� ��ϸ� �����ϸ� �׳� ��ġ
//          �����̴� ��ϸ� �����ϸ�, WayPoint�� �ڽ� ������Ʈ�� ������ , �� �ڽĿ�����Ʈ��  Position�� ������� ���󰡰�
//          ������ ���� ��� ���ƿ´�.
public class SpikeWheel : MonoBehaviour
{
    public GameObject wayPoint; // �θ� ������Ʈ�� ��θ� ���� ������Ʈ
    List<Transform> chilWayPoints = new List<Transform>();      //  �ڽ� ������Ʈ�� ��ε��� ��ġ�� �޾ƿ� ����Ʈ
    public float wheelTurnSpeed; // 1�ʴ� ������ ���� Ƚ��
    Vector3 startPoint; //  �ڽ� ������Ʈ�� ����� ���� ó���� ����ִ� ����Ʈ
    Vector3 endPoint;   //                  ''          �������� ����ִ� ����Ʈ                 
    int curWayPoint;    // ���� ������Ʈ�� ������ ����Ʈ�� �ε���
    float elapsedTime;  //  ������ ������
    Transform initPos;
    float moveTime;     
    float moveDisPer;
    bool isReturning;  //end waypoint���� ���ư��� ���

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
            chilWayPoints.Clear(); // Ȥ�� Start ���� �� �� �� ������ �ʱ�ȭ
            foreach (Transform t in wayPoint.transform)
                chilWayPoints.Add(t);

            startPoint = chilWayPoints[0].position;
            endPoint = chilWayPoints[1].position;
        }
        else
        {
            // �ƹ� �͵� �� �ϰ� ���� ��ġ ���� (�Ǵ� ���ϴ� �⺻ ��ġ)
            startPoint = transform.position;
            endPoint = transform.position;
        }
        transform.position = startPoint;
    }

    void Update()
    {
        SpinWheel();     // ������ �����ϴ� �Լ�
        SetNextTarget(); // ��ġ�� �����ϴ� �Լ� 
        MoveWayPoint();  // ��ϸ� �����̰��ϴ� �Լ�
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
            Debug.Log("0 ���� ���� : " + curWayPoint);
            curWayPoint = 1;
        }
        else if (curWayPoint > chilWayPoints.Count - 1)
        {
            isReturning = true;
            curWayPoint = chilWayPoints.Count - 2;
            Debug.Log("0 ���� ŭ!");
        }
        else
            Debug.Log("��������Ʈ ���� ��!!");

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
        // �÷��̾�� ��ȣ�ۿ� ���� ��
        if (collision.tag=="Player")
        {
            collision.gameObject.GetComponent<Player>().isDead = true;
        }
    }
}
