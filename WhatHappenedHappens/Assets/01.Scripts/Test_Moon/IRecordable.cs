using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecordable
{
    // 현재 위치를 기록하는 메서드
    void RecordPosition(float time);

    // 이동 기록을 설정하는 메서드
    void SetMovementRecords(List<ObjectMovementRecord> records);

    // 저장된 이동 기록을 가져오는 메서드
    List<ObjectMovementRecord> GetMovementRecords();

    // 이동 기록을 재생하는 메서드
    IEnumerator ReplayMovement();
}
