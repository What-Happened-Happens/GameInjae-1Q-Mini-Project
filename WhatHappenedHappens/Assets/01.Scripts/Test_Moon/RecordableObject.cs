using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecordableObject : MonoBehaviour, IRecordable
{
    protected List<ObjectMovementRecord> movementRecords = new();

    public virtual void RecordPosition(float time)
    {
        movementRecords.Add(new ObjectMovementRecord(time, transform.position));
    }

    public virtual List<ObjectMovementRecord> GetMovementRecords()
    {
        return new List<ObjectMovementRecord>(movementRecords);
    }

    public virtual void SetMovementRecords(List<ObjectMovementRecord> records)
    {
        movementRecords = records;
    }

    public virtual IEnumerator ReplayMovement()
    {
        for (int i = 1; i < movementRecords.Count; i++)
        {
            float waitTime = movementRecords[i].time - movementRecords[i - 1].time;
            Vector3 start = movementRecords[i - 1].position;
            Vector3 end = movementRecords[i].position;

            float elapsed = 0f;
            while (elapsed < waitTime)
            {
                transform.position = Vector3.Lerp(start, end, elapsed / waitTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = end;
        }
    }
}