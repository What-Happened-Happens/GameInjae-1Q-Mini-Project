using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [ 공 이벤트 ]
// 
public class Button_Ball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("[Ball] Player가 버튼에 닿았습니다.");

        Ball ball = FindObjectOfType<Ball>();
        if (ball != null)
        {
            ball.MoveUp(); // 공 이동

            float eventTime = Time.time - ParadoxManager.Instance.recordingStartTime;

            // IRecordable 객체들의 위치 기록
            foreach (var obj in FindObjectsOfType<MonoBehaviour>().OfType<IRecordable>())
            {
                obj.RecordPosition(eventTime);
            }
        }
        else
        {
            Debug.LogWarning("[Button_Ball] Ball 오브젝트를 찾을 수 없습니다.");
        }
    }
}
