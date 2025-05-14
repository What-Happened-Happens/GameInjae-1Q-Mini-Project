using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputRecord
{
    public float timestamp;
    public Vector2 moveDirection;  // 방향 입력 (A, D 키)
    public bool jump;              // 점프 입력
    public Vector3 position;       // 초기 위치
    public float difftime;

    public KeyInputRecord(float time, Vector2 dir)
    {
        timestamp = time;
        moveDirection = dir;
    }

    public KeyInputRecord(float time, Vector2 dir, Vector3 pos, float diff_time)
    {
        timestamp = time;
        moveDirection = dir;
        position = pos; // 초기 위치를 저장할 변수
        difftime = diff_time;
    }

}


// 할것!!!
// 끝에 isDoubleStart, iskStart false만들기1!!

public class Interect_manager : MonoBehaviour
{
    public GameObject Player;
    public GameObject copyObject;

    bool isKStart; // r을 1번 눌렀을때 T 끝나면 f
    bool isDoubleStart; // r을 시간 내에 2번 눌렀을때 T
    //float eliepseTime;
    float copyTotalTime;
    int copyCount;
    int copyIndex;
    int copyMax;
    float moveSpeed;
    float jumpForce;

    int[] nowExplainIndex = new int[4];
    int[] nextTimeExplainIndex = new int[4];
    GameObject[] copyInstant = new GameObject[4];
    List<KeyInputRecord>[] keyInputArray = new List<KeyInputRecord>[4];
    bool isInstance;

    void Start()
    {
        copyTotalTime = 0;
        isKStart = false;
        isInstance = false;
        isDoubleStart = false;
        copyCount = 0;
        moveSpeed = Player.GetComponent<MoveOverTime>().moveSpeed;
        jumpForce = Player.GetComponent<MoveOverTime>().jumpForce;

        for (int i = 0; i < 4; i++)
        {
            keyInputArray[i] = new List<KeyInputRecord>();
            nowExplainIndex[i] = 0;
        }
    }

    void Update()
    {
        
        CopyStartTime();
        KeySave();
        CopyInstant();
        KeyExplain();
        CopyDestroy();
        endCopy();
    }

    void CopyStartTime()
    {
        // R 버튼을 눌렀을 때
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(isDoubleStart == true) return;
            if (isKStart == true) // 버튼을 시간 안에 한번 더 눌렀을때, 최근 분신의 diffTime을 모든 시간에 빼서 정상화!!
            {
                isDoubleStart = true;
                keyInputArray[copyIndex].Clear();
                Vector3 startPos = Player.transform.position;//처음 시간 넣기
                keyInputArray[copyIndex].Add(new KeyInputRecord(0f, Vector2.zero, startPos, copyTotalTime));
                float standardStartTime = keyInputArray[copyIndex][0].difftime;
               
                // 현재 상태의 pos를 다음 startpos로 사용할 수 있도록 startpos에 pos를 저장
                for (int i = 0; i < copyMax; i++)
                {
                    if (i == copyIndex)
                    {
                        Debug.Log("10");
                        continue;
                    }
                    if (i == (copyCount - 1) % 4)
                    {
                        Debug.Log("11");
                        continue;
                    }
                    keyInputArray[i][0].position = copyInstant[i].transform.position; // 현재 분신의 위치를 리스트의 첫번쩨 위치에 저장!!!
                }

                // r이 한번 더 눌렸을때 다른 분신의 출발시간에 최근 저장된 분신의 startTime을 더해줌
                for (int i = 0; i < copyMax; i++)  
                {
                    if(i == copyIndex) {
                        Debug.Log("12");
                        continue;
                    }
                    if (i == (copyCount - 1) % 4)
                    {
                        Debug.Log("13");
                        continue;
                    }
                    // 최근 분신의 출발 시간을 다른 분신의 출발시간에 더해줌
                    keyInputArray[i][0].difftime += keyInputArray[(copyCount-1)%4][0].difftime; 
                    // 분신의 마지막 시간일때보다 diffTime이 클때 생성 안되게하는것은 instant 함수에 넣을것임!!

                }

                // 출발시간이 정렬된 상태의 각각의 인덱스를 nextTimeExplainIndex에 저장할것임!!
                for (int i = 0; i < copyMax; i++) {
                    if (i == copyIndex)
                    {
                        Debug.Log($"instantate {i}번째 실행됨!");
                        continue;
                    }
                    //각각의 분신의 시작시간보다 1인덱스 작거나 같을때 그때의 인덱스 값을 nextTimeExplainIndex에 저장!!!
                    for (int j = 0; j < keyInputArray[i].Count; j++)
                    {
                        if (keyInputArray[i][j].timestamp >= keyInputArray[i][0].difftime)
                        {
                            nextTimeExplainIndex[i] = j;
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log($"124");
                isKStart = true;
                copyIndex = copyCount % 4;
                keyInputArray[copyIndex].Clear();
                Vector3 startPos = Player.transform.position;//처음 시간 넣기
                keyInputArray[copyIndex].Add(new KeyInputRecord(0f, Vector2.zero, startPos, 0f));
                copyCount++;
                copyMax = Mathf.Min(copyCount, 4);
                nowExplainIndex[copyIndex] = 0;
            }
            
        }

        if (isKStart)
        {
            copyTotalTime += Time.deltaTime;
        }

        //나중에 수정하기!!!
        
    }

    void KeySave()
    {
        // 조건 수정하기!!!

        if (isDoubleStart == true)
        {
            Vector2 copyVect = Player.GetComponent<Rigidbody2D>().velocity;
            keyInputArray[copyIndex].Add(new KeyInputRecord(copyTotalTime, copyVect));
        }
        else if (isKStart && copyTotalTime <= 5)
        {
            //float horizontal = 0f;
            //if (Input.GetKey(KeyCode.A)) horizontal = -1f;
            //else if (Input.GetKey(KeyCode.D)) horizontal = 1f;
            //bool isJump = false;
            //Vector2 moveDir = new Vector2(horizontal, 0);
            Vector2 copyVect = Player.GetComponent<Rigidbody2D>().velocity;
            keyInputArray[copyIndex].Add(new KeyInputRecord(copyTotalTime, copyVect));
        }
    }

    void CopyInstant()
    {

        if (!isInstance && isKStart && copyCount > 1)
        {
            for (int i = 0; i < copyMax; i++)
            {
                if (i == copyIndex) {
                    Debug.Log($"instantate {i}번째 실행됨!");
                    continue;
                }
                   
                if (keyInputArray[i] == null || keyInputArray[i].Count == 0) continue;


                copyInstant[i] = Instantiate(copyObject, keyInputArray[i][0].position, Quaternion.identity);
            }
            isInstance = true;
        }
    }

    void KeyExplain()
    {
        if (!isKStart) return;

        for (int i = 0; i < copyMax; i++)
        {
            if (i == copyIndex) continue;
            //if (keyInputArray[i] == null || keyInputArray[i].Count <= nowExplainIndex[i]) continue;
            if (copyInstant[i] == null) continue;
            Debug.Log($"{i}번 실행!!");
            // 현재 실행중인 인덱스가 전체 배열의 개수보다 작고, 현재 실행중인 시간이 copyTotalTime보다 작다면 계속 실행
            while(nowExplainIndex[i] < keyInputArray[i].Count &&
               copyTotalTime >= keyInputArray[i][nowExplainIndex[i]].timestamp)
            {
                var record = keyInputArray[i][nowExplainIndex[i]];
                Rigidbody2D rb = copyInstant[i].GetComponent<Rigidbody2D>();
                if (rb == null) {
                    Debug.Log($"{i}번째가 생성 안됨!!");
                    continue;
                }

                Vector2 velocity = rb.velocity;
                velocity.x = record.moveDirection.x;
                velocity.y = record.moveDirection.y;

 
                rb.velocity = velocity;
                nowExplainIndex[i]++;
            }
        }
    }
    void CopyDestroy()
    {
        for (int i = 0; i < copyMax; i++)
        {
            if (i == copyIndex) continue;
            if (nowExplainIndex[i] >= keyInputArray[i].Count)
            {
                Debug.Log("삭제!!!");
                Destroy(copyInstant[i]);
            }
        }
        if (isKStart && copyTotalTime > 5 && copyCount == 1)
        {
            copyTotalTime = 0;
            isKStart = false;
            isDoubleStart = false;
            Destroy(copyInstant[0]);

            Debug.Log("처음 5초 끝남!!");
            isInstance = false;
        }
    }

    void endCopy()
    {
        if (isKStart && copyTotalTime > keyInputArray[(copyCount-1)%4][0].difftime + 5 && copyCount > 1)
        {
            copyTotalTime = 0;
            isKStart = false;
            isDoubleStart = false;
            for (int i = 0; i < copyMax; i++)
            {
                if (i == copyIndex) continue;
                if (i == (copyCount - 1) % 4) continue;
                nowExplainIndex[i] = nextTimeExplainIndex[i];
            }

            Debug.Log("5초 끝남");
            isInstance = false;
        }

    }
}