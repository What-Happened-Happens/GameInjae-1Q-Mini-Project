using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    public bool reverseAtEnd = true; // 끝에서 반대방향으로
    public bool loop = false;        // 끝에서 멈추지 않고 계속 반복
    public bool alwaysOn = false;    // 항상 작동할지

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
                isActive = false; // 정지
            }
        }
    }

    public void SetActive(bool value)
    {
        if (alwaysOn) return;
        isActive = value;
    }
}
