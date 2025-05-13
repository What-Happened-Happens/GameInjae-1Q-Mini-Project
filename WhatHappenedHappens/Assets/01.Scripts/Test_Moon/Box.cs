using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IRecordable
{
    private Vector3 startPosition;
    public float moveDistance = 2f;
    public float moveDuration = 1f;

    private List<ObjectMovementRecord> movementRecords = new List<ObjectMovementRecord>();

    private void Start()
    {
        startPosition = transform.position;
    }

    public void MoveRight()
    {
        StopAllCoroutines();
        StartCoroutine(MoveSmoothly());
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }

    private IEnumerator MoveSmoothly()
    {
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.right * moveDistance;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
    }

    // --- IRecordable 구현부 ---

    public void RecordPosition(float time)
    {
        // 현재 위치를 기록
        movementRecords.Add(new ObjectMovementRecord(time, transform.position));
    }

    public void SetMovementRecords(List<ObjectMovementRecord> records)
    {
        movementRecords = new List<ObjectMovementRecord>();

        foreach (var record in records)
        {
            movementRecords.Add(new ObjectMovementRecord(record.time, record.position));
        }
    }

    public List<ObjectMovementRecord> GetMovementRecords()
    {
        return movementRecords;
    }

    public IEnumerator ReplayMovement()
    {
        if (movementRecords.Count == 0) yield break;

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