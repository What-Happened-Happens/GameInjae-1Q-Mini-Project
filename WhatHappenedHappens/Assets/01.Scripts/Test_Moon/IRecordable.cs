using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecordable
{
    // ���� ��ġ�� ����ϴ� �޼���
    void RecordPosition(float time);

    // �̵� ����� �����ϴ� �޼���
    void SetMovementRecords(List<ObjectMovementRecord> records);

    // ����� �̵� ����� �������� �޼���
    List<ObjectMovementRecord> GetMovementRecords();

    // �̵� ����� ����ϴ� �޼���
    IEnumerator ReplayMovement();
}
