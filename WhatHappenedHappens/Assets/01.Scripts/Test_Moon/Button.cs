using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ �ڽ� �̺�Ʈ ]
// 
public class Button : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Box box = FindObjectOfType<Box>();
            if (box != null)
            {
                box.MoveRight();

                float eventTime = Time.time - ParadoxManager.Instance.recordingStartTime;
                var ev = new ParadoxEvent(eventTime, box.gameObject, ActionType.MoveBox);
                ParadoxManager.Instance.RecordEvent(ev);
            }
        }
    }
}
