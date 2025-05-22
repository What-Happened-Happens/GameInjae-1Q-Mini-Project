using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item_Rotate : MonoBehaviour
{
    [Header("Card Setting")]
    public float rotateSpeed = 20;
    public float rotateTime = 2;

    private bool isEntered = false;
    private float elapsedTime = 0f;

    private Vector3 directionVector = Vector3.zero;
    private bool isMoving = false;

    private AudioSource audioSource;

    private Vector3 startPos;
    private float moveDistance = 10f; // 얼마나 이동하면 파괴할지 기준

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isEntered) return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime <= rotateTime)
        {
            // 제자리 회전
            transform.rotation *= Quaternion.Euler(0, rotateSpeed * Time.deltaTime * 100f, 0);
        }
        else
        {
            // 회전 멈추기
            transform.rotation = Quaternion.identity;

            // 이동 시작 (한 번만 방향 계산)
            if (!isMoving)
            {
                directionVector = new Vector3(-1, 0.5f, 0).normalized; // 왼쪽 위 방향
                startPos = transform.position;
                isMoving = true;
            }

            // 이동
            transform.position += directionVector * Time.deltaTime * 15f;

            // 일정 거리 이상 이동하면 제거
            if (Vector3.Distance(startPos, transform.position) > moveDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEntered = true;
            audioSource.Play();

            collision.gameObject.GetComponent<Player>().hasCardKey = true;

            foreach (var col in GetComponents<Collider2D>())
            {
                col.enabled = false;
            }

            // 필요하다면 Rigidbody2D도 비활성화
            // GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
