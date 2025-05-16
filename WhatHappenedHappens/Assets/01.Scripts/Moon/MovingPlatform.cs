using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    public bool reverseAtEnd = true; // ������ �ݴ��������
    public bool loop = false;        // ������ ������ �ʰ� ��� �ݺ�
    public bool alwaysOn = false;    // �׻� �۵�����

    private bool isActive = false;
    private Vector3 target;
    private Vector3 direction;
    private bool movingToB = true;

    private void Start()
    {
        target = pointB.position;
        direction = (pointB.position - pointA.position).normalized;
    }

    private void Update()
    {
        if (!isActive && !alwaysOn) return;

        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            if (reverseAtEnd)
            {
                movingToB = !movingToB;
                target = movingToB ? pointB.position : pointA.position;
                direction = (target - transform.position).normalized;
            }
            else if (loop)
            {
                transform.position = pointA.position;
                target = pointB.position;
                direction = (target - transform.position).normalized;
            }
            else
            {
                isActive = false; // ����
            }
        }
    }

    public void SetActive(bool value)
    {
        if (alwaysOn) return;
        isActive = value;
    }
}
