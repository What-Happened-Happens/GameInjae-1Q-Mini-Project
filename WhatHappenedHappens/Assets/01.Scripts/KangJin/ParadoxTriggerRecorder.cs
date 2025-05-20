using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParadoxTriggerRecorder : MonoBehaviour
{
    //트리거 오브젝트의 상태값을 부모클래스로 저장하는 리스트
    private List<ObjectTriggerRecord> triggers = new List<ObjectTriggerRecord>();

    private Queue<List<ObjectTriggerRecord>> triggersqueue = new Queue<List<ObjectTriggerRecord>>();

    private float lastRecordTime = 0f;

    public void Start()
    {
        triggers.Clear();
        lastRecordTime = 0f;
    }

    public void Record(TrueFalse tf, float elapsed)
    {
        if(elapsed - lastRecordTime >=0.1f)
        {
            //시간별로 상태값 저장
            triggers.Add(new ObjectTriggerRecord(elapsed,tf.IsTrue));

            //lastRecordTime 추가
            lastRecordTime = elapsed;
        }
    }

    public void Enqueue(int maxCount)
    {
        /*if(triggersqueue.Count >= maxCount) {triggersqueue.Dequeue();}*/
        triggersqueue.Enqueue(new List<ObjectTriggerRecord>(triggers));
    }

    public void Clear()
    {
        triggersqueue.Clear();
    }

    public void Trim(float timePassed)
    {
        triggersqueue = new Queue<List<ObjectTriggerRecord>>(triggersqueue.Select(list =>
        list.Where(r => r.time >= timePassed)
            .Select(r => new ObjectTriggerRecord(r.time - timePassed, r.isTrue))
            .ToList()));
    }

    public int GetTriggerQueueCount()
    {
        return  triggersqueue.Count;
    }

    public List<List<ObjectTriggerRecord>> GetAllTriggerData() => new List<List<ObjectTriggerRecord>>(triggersqueue);
}
