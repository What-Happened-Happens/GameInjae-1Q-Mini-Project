using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [ �ڽ� �̺�Ʈ ]
// 
public class Button : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("[Button] Player�� ��ư�� ��ҽ��ϴ�.");

        Box box = FindObjectOfType<Box>();
        if (box != null)
        {
            box.MoveRight(); // �ڽ� �̵�

            float eventTime = Time.time - ParadoxManager.Instance.recordingStartTime;

            // IRecordable ��ü���� ��ġ ���
            foreach (var obj in FindObjectsOfType<MonoBehaviour>().OfType<IRecordable>())
            {
                obj.RecordPosition(eventTime);
            }
        }
        else
        {
            Debug.LogWarning("[Button] Box ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
