using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convayor_OnOff : MonoBehaviour
{
    public TrueFalse truefalse;
    public float moveSpeed = 3f;     // �����̾� ��Ʈ �ӵ�
    public int moveX = -1;            // 1: ������, -1: ����

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.rigidbody; // �� �����ϰ� ���� ����

            if (rb != null && !rb.isKinematic) /*return;*/
            {
                if (!truefalse.IsTrue)
                {
                    rb.AddForce(new Vector2(moveX * moveSpeed * 50f, 0f));
                }
            }
        }
    }
}
