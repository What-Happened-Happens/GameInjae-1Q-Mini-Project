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
    public RectTransform targetUI; // ��ǥ UI ������Ʈ

    private bool isEntered = false;
    private float elapsedTime = 0f;

    private Vector3 targetPos;
    private Vector3 directionVector;
    private bool isMoving = false;

    private AudioSource audioSource;

    void Start()
    {
        // ȸ�� �� �̵��� ��ǥ UI�� ���� ��ġ
        // targetPos = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.9f, Camera.main.nearClipPlane));
        // targetPos.z = 0; // Z�� ����

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isEntered) return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime <= rotateTime)
        {
            // ���ڸ� ȸ��
            transform.rotation *= Quaternion.Euler(0, rotateSpeed * Time.deltaTime * 100f, 0);
            
        }
        else
        {
            // ȸ�� ���߱�
            transform.rotation = Quaternion.identity;

            // �̵� ���� (�� ���� ���� ���)
            if (!isMoving)
            {
                // UI�� ��ũ�� ��ġ �� ���� ��ġ�� ��ȯ
                Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetUI.position);
                screenPos.z = Camera.main.nearClipPlane;

                targetPos = Camera.main.ScreenToWorldPoint(screenPos);
                targetPos.z = 0;

                directionVector = (targetPos - transform.position).normalized;
                isMoving = true;
            }

            // ��ǥ �������� �̵�
            transform.position += directionVector * Time.deltaTime * 15f;

            // �Ÿ� ��������� ����
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

            // �� ������Ʈ�� ���� ��� Collider2D�� ��Ȱ��ȭ
            foreach (var col in GetComponents<Collider2D>())
            {
                col.enabled = false;
            }

            // �ʿ��ϴٸ� Rigidbody2D�� ��Ȱ��ȭ�ؼ� ���� ���� ���� 
            // GetComponent<Rigidbody2D>().simulated = false;

        }
    }
}
