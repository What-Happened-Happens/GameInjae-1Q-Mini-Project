using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;


public class SpikeWheel : MonoBehaviour
{
    public float wheelTurnSpeed; // 1�ʴ� ������ ���� Ƚ��
    Vector3 startPoint; //  �ڽ� ������Ʈ�� ����� ���� ó���� ����ִ� ����Ʈ

    void Start()
    {
        wheelTurnSpeed = 360f;
    }

    void Update()
    {
        SpinWheel();     // ������ �����ϴ� �Լ�
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
        // �÷��̾�� ��ȣ�ۿ� ���� ��
        if (collision.tag=="Player")
        {
            collision.gameObject.GetComponent<Player>().isDead = true;
        }
    }
}
