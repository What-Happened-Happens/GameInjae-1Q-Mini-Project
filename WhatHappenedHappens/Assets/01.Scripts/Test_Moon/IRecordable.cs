using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecordable
{
    void RecordPosition(float time); // 현재 위치 기록 
    void SetMovementRecords(List<ObjectMovementRecord> records); // 이동 기록 
    List<ObjectMovementRecord> GetMovementRecords(); // 이동 기록 가져오기

    IEnumerator ReplayMovement(); // 이동 기록 재생 
}
