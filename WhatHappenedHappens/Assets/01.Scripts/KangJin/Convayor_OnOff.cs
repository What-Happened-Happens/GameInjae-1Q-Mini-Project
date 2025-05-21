using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convayor_OnOff : MonoBehaviour
{
    public TrueFalse truefalse;
    public float moveSpeed = 3f;     // 컨베이어 벨트 속도
    public int moveX = -1;            // 1: 오른쪽, -1: 왼쪽

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.rigidbody; // 더 안전하고 빠른 접근

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
