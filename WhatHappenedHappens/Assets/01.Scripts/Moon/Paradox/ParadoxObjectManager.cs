using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxObjectManager : MonoBehaviour
{
    [Header("Moving Objects")]
    public List<Transform> movingObjects = new List<Transform>();
    private List<Vector3> startPositions = new List<Vector3>();

    [Header("Trigger Objects")] // 트리거 오브젝트들의 기존 상태 저장 

    [Header("Player Start Position")]
    private Vector3 playerStartPos;

    public void Save()
    {
        startPositions.Clear();
        foreach (var obj in movingObjects)
        {
            if (obj != null) startPositions.Add(obj.position);
        }
    }

    public void ResetAll()
    {
        for (int i = 0; i < movingObjects.Count; i++)
        {
            if (movingObjects[i] != null)
                movingObjects[i].position = startPositions[i];
        }
    }

    public void SavePlayer(GameObject player)
    {
        playerStartPos = player.transform.position;
    }

    public void ResetPlayer(GameObject player)
    {
        player.transform.position = playerStartPos;
    }
}