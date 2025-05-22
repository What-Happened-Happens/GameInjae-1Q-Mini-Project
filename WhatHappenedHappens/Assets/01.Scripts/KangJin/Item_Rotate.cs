using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item_Rotate : MonoBehaviour
{
    [Header("Card Setting")]
    public float rotateSpeed = 20;
    public float rotateTime = 2;

    [Header("Target UI")]
    public RectTransform targetUI; // 목표 UI 오브젝트

    private bool isEntered = false;
    private float elapsedTime = 0f;

    private Vector3 targetPos;
    private Vector3 directionVector;
    private bool isMoving = false;

    private AudioSource audioSource;

    void Start()
    {
        // 회전 후 이동할 목표 UI의 월드 위치
        // targetPos = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.9f, Camera.main.nearClipPlane));
        // targetPos.z = 0; // Z값 고정

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
                // UI의 스크린 위치 → 월드 위치로 변환
                Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetUI.position);
                screenPos.z = Camera.main.nearClipPlane;

                targetPos = Camera.main.ScreenToWorldPoint(screenPos);
                targetPos.z = 0;

                directionVector = (targetPos - transform.position).normalized;
                isMoving = true;
            }

            // 목표 방향으로 이동
            transform.position += directionVector * Time.deltaTime * 15f;

            // 거리 가까워지면 제거
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
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

            // 이 오브젝트에 붙은 모든 Collider2D를 비활성화
            foreach (var col in GetComponents<Collider2D>())
            {
                col.enabled = false;
            }

            // 필요하다면 Rigidbody2D도 비활성화해서 물리 반응 제거 
            // GetComponent<Rigidbody2D>().simulated = false;

        }
    }
}
