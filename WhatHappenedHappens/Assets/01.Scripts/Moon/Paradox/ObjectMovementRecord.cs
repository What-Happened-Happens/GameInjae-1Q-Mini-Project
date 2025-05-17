using UnityEngine;

[System.Serializable]
public class ObjectMovementRecord
{
    public float time;
    public Vector3 position;

    public ObjectMovementRecord(float time, Vector3 position)
    {
        this.time = time;
        this.position = position;
    }
}
