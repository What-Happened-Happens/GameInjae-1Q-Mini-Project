using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [ �� �̺�Ʈ ]
// 
public class Button_Ball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("[Ball] Player�� ��ư�� ��ҽ��ϴ�.");

        Ball ball = FindObjectOfType<Ball>();
        if (ball != null)
        {
            ball.MoveUp(); // �� �̵�

            float eventTime = Time.time - ParadoxManager.Instance.recordingStartTime;

            // IRecordable ��ü���� ��ġ ���
            foreach (var obj in FindObjectsOfType<MonoBehaviour>().OfType<IRecordable>())
            {
                obj.RecordPosition(eventTime);
            }
        }
        else
        {
            Debug.LogWarning("[Button_Ball] Ball ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
