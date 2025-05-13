using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [ 박스 이벤트 ]
// 
public class Button : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("[Button] Player가 버튼에 닿았습니다.");

        Box box = FindObjectOfType<Box>();
        if (box != null)
        {
            box.MoveRight(); // 박스 이동

            float eventTime = Time.time - ParadoxManager.Instance.recordingStartTime;

            // IRecordable 객체들의 위치 기록
            foreach (var obj in FindObjectsOfType<MonoBehaviour>().OfType<IRecordable>())
            {
                obj.RecordPosition(eventTime);
            }
        }
        else
        {
            Debug.LogWarning("[Button] Box 오브젝트를 찾을 수 없습니다.");
        }
    }
}
