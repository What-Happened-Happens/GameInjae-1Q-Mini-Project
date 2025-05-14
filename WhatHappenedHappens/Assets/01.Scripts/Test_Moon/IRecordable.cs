using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecordable
{
    void RecordPosition(float time); // ���� ��ġ ��� 
    void SetMovementRecords(List<ObjectMovementRecord> records); // �̵� ��� 
    List<ObjectMovementRecord> GetMovementRecords(); // �̵� ��� ��������

    IEnumerator ReplayMovement(); // �̵� ��� ��� 
}
