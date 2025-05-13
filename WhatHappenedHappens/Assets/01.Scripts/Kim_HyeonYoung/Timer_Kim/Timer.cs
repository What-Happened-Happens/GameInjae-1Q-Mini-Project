using Script.Interface;
using UnityEngine;

public class Timer : MonoBehaviour, ITimer
{
    private float StartTime = 0f;
    private float DeltaTime = 0f;
    private float lastTime = 0f; 

    public void TimeInit(bool CloneInput)
    {
        StartTime = Time.time; //  Now Time 
        DeltaTime = Time.deltaTime;
        lastTime = StartTime; 
    }
    public void Update()
    {
        float now = Time.time;
        DeltaTime = Time.deltaTime;
        lastTime = now; 
    }
    public float GetDeltaTime()
    {
        return DeltaTime; 
    }

    public bool IsElapsed(float duration) // current Time ��� �� �ð� Check 
    {
        return (Time.time - StartTime) >= duration;
    }

    public void ResetTimer()
    {     
        StartTime = Time.time - DeltaTime;
        Debug.Log($"Tiemr Reset! last Update Time : {StartTime}");
    }
}
