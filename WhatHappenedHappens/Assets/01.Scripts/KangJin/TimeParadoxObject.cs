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
        //���� ���� �޾ƿͼ� ���������ϰ� �ٸ���, �� �ð� �� ���¸� ����
        currState = tf.IsTrue;

        if (Input.GetKeyDown(KeyCode.R))
        {
            lastRecordStartTime = Time.time;
        }

        // ��ȭ���� ��
        if(otm.recording)
        {
            elapsedTime += Time.deltaTime;
        }

        // ������� ��
        if(otm.Replaying)
        {
            
        }
    }

    void CheckState()
    {
        //elapsedTime ������Ű��, �� 
        currState = tf.IsTrue;
        if (currState != prevState)
        {
            //�ð��� bool �� ����
            /*ObjectTriggerRecord otr = new ObjectTriggerRecord()*/
            //���� �� ���� ť�� �ֱ�

            //�� �� �̻� �������� ��,

            //���������� ������ �� ����

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
