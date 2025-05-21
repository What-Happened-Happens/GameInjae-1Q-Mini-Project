using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxObjectManager : MonoBehaviour
{
    [Header("Moving Objects")] // 움직이는 오브젝트들
    public List<Transform> movingObjects = new List<Transform>();
    private List<Vector3> startPositions = new List<Vector3>();

    [Header("Trigger Objects")] // 트리거 오브젝트들
    public List<GameObject> triggerObjects = new List<GameObject>();
    private List<bool> triggerStates = new List<bool>();

    [Header("Player Start Position")]
    private Vector3 playerStartPos;

    public void Save()
    {
        startPositions.Clear();
        triggerStates.Clear();

        foreach (var obj in movingObjects)
        {
            if (obj != null) startPositions.Add(obj.position);
        }

        foreach (var trigger in triggerObjects)
        {
            if (trigger != null)
            {
                TrueFalse tf = trigger.GetComponent<TrueFalse>();
                if (tf != null) triggerStates.Add(tf.IsTrue);
                else triggerStates.Add(false); // 기본값
            }
        }
    }

    public void ResetAll()
    {
        for (int i = 0; i < movingObjects.Count; i++)
        {
            if (movingObjects[i] != null)  movingObjects[i].position = startPositions[i];
        }

        for (int i = 0; i < triggerObjects.Count; i++)
        {
            var trigger = triggerObjects[i];
            if (trigger != null)
            {
                TrueFalse tf = trigger.GetComponent<TrueFalse>();
                if (tf != null && i < triggerStates.Count)
                {
                    tf.SetState(triggerStates[i]);
                }
            }
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