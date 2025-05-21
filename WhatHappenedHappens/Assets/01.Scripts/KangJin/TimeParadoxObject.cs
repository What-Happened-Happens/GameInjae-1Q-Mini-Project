using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeParadoxObject : MonoBehaviour
{
    // Start is called before the first frame update
    float timer;
    public ObjectTriggerManager otm;
    TrueFalse tf;
    bool prevState;
    bool currState;
    Queue<ObjectTriggerRecord> objectTriggerRecord;
    float lastRecordStartTime;
    float lastRecordedTime;
    float elapsedTime;
    void Start()
    {
        objectTriggerRecord = new Queue<ObjectTriggerRecord>();
        timer = 0;
        tf = gameObject.GetComponent<TrueFalse>();
        prevState = currState = tf.IsTrue;
        lastRecordStartTime = 0;
        lastRecordedTime = 0;
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;
        //현재 상태 받아와서 이전상태하고 다르면, 그 시간 및 상태를 저장
        currState = tf.IsTrue;

        if (Input.GetKeyDown(KeyCode.R))
        {
            lastRecordStartTime = Time.time;
        }

        // 녹화중일 때
        if(otm.recording)
        {
            elapsedTime += Time.deltaTime;
        }

        // 재생중일 때
        if(otm.Replaying)
        {
            
        }
    }

    void CheckState()
    {
        //elapsedTime 증가시키고, 그 
        currState = tf.IsTrue;
        if (currState != prevState)
        {
            //시간과 bool 값 저장
            /*ObjectTriggerRecord otr = new ObjectTriggerRecord()*/
            //저장 한 값을 큐에 넣기

            //한 개 이상 저장했을 때,

            //마지막으로 저장한 값 저장

        }
    }

    /*void Replay()
    {
        if (objectTriggerRecord.Count > 0)
        {
            foreach (var trigger in objectTriggerRecord)
            {

            }
        }
    }*/
}
