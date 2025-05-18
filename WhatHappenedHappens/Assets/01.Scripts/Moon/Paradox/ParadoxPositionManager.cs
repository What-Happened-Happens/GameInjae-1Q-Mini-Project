using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxPositionManager : MonoBehaviour
{
    [Header("Track Objects")]
    public List<Transform> trackedObjects = new List<Transform>();

    private List<Vector3> startPositions = new List<Vector3>();
    private Vector3 playerStartPos;

    public void Save()
    {
        startPositions.Clear();
        foreach (var obj in trackedObjects)
        {
            if (obj != null)
                startPositions.Add(obj.position);
        }
    }

    public void ResetAll()
    {
        for (int i = 0; i < trackedObjects.Count; i++)
        {
            if (trackedObjects[i] != null)
                trackedObjects[i].position = startPositions[i];
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