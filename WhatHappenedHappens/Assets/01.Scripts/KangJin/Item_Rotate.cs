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
    private float moveDistance = 10f; // �󸶳� �̵��ϸ� �ı����� ����

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
                directionVector = new Vector3(-1, 0.5f, 0).normalized; // ���� �� ����
                startPos = transform.position;
                isMoving = true;
            }

            // �̵�
            transform.position += directionVector * Time.deltaTime * 15f;

            // ���� �Ÿ� �̻� �̵��ϸ� ����
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

            // �ʿ��ϴٸ� Rigidbody2D�� ��Ȱ��ȭ
            // GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
