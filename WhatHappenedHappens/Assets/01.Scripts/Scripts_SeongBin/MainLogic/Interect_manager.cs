using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputRecord
{
    public float timestamp;
    public Vector2 moveDirection;  // ���� �Է� (A, D Ű)
    public bool jump;              // ���� �Է�
    public Vector3 position;       // �ʱ� ��ġ
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
        position = pos; // �ʱ� ��ġ�� ������ ����
        difftime = diff_time;
    }

}


// �Ұ�!!!
// ���� isDoubleStart, iskStart false�����1!!

public class Interect_manager : MonoBehaviour
{
    public GameObject Player;
    public GameObject copyObject;

    bool isKStart; // r�� 1�� �������� T ������ f
    bool isDoubleStart; // r�� �ð� ���� 2�� �������� T
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
        // R ��ư�� ������ ��
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(isDoubleStart == true) return;
            if (isKStart == true) // ��ư�� �ð� �ȿ� �ѹ� �� ��������, �ֱ� �н��� diffTime�� ��� �ð��� ���� ����ȭ!!
            {
                isDoubleStart = true;
                keyInputArray[copyIndex].Clear();
                Vector3 startPos = Player.transform.position;//ó�� �ð� �ֱ�
                keyInputArray[copyIndex].Add(new KeyInputRecord(0f, Vector2.zero, startPos, copyTotalTime));
                float standardStartTime = keyInputArray[copyIndex][0].difftime;
               
                // ���� ������ pos�� ���� startpos�� ����� �� �ֵ��� startpos�� pos�� ����
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
                    keyInputArray[i][0].position = copyInstant[i].transform.position; // ���� �н��� ��ġ�� ����Ʈ�� ù���� ��ġ�� ����!!!
                }

                // r�� �ѹ� �� �������� �ٸ� �н��� ��߽ð��� �ֱ� ����� �н��� startTime�� ������
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
                    // �ֱ� �н��� ��� �ð��� �ٸ� �н��� ��߽ð��� ������
                    keyInputArray[i][0].difftime += keyInputArray[(copyCount-1)%4][0].difftime; 
                    // �н��� ������ �ð��϶����� diffTime�� Ŭ�� ���� �ȵǰ��ϴ°��� instant �Լ��� ��������!!

                }

                // ��߽ð��� ���ĵ� ������ ������ �ε����� nextTimeExplainIndex�� �����Ұ���!!
                for (int i = 0; i < copyMax; i++) {
                    if (i == copyIndex)
                    {
                        Debug.Log($"instantate {i}��° �����!");
                        continue;
                    }
                    //������ �н��� ���۽ð����� 1�ε��� �۰ų� ������ �׶��� �ε��� ���� nextTimeExplainIndex�� ����!!!
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
                Vector3 startPos = Player.transform.position;//ó�� �ð� �ֱ�
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

        //���߿� �����ϱ�!!!
        
    }

    void KeySave()
    {
        // ���� �����ϱ�!!!

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
                    Debug.Log($"instantate {i}��° �����!");
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
            Debug.Log($"{i}�� ����!!");
            // ���� �������� �ε����� ��ü �迭�� �������� �۰�, ���� �������� �ð��� copyTotalTime���� �۴ٸ� ��� ����
            while(nowExplainIndex[i] < keyInputArray[i].Count &&
               copyTotalTime >= keyInputArray[i][nowExplainIndex[i]].timestamp)
            {
                var record = keyInputArray[i][nowExplainIndex[i]];
                Rigidbody2D rb = copyInstant[i].GetComponent<Rigidbody2D>();
                if (rb == null) {
                    Debug.Log($"{i}��°�� ���� �ȵ�!!");
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
                Debug.Log("����!!!");
                Destroy(copyInstant[i]);
            }
        }
        if (isKStart && copyTotalTime > 5 && copyCount == 1)
        {
            copyTotalTime = 0;
            isKStart = false;
            isDoubleStart = false;
            Destroy(copyInstant[0]);

            Debug.Log("ó�� 5�� ����!!");
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

            Debug.Log("5�� ����");
            isInstance = false;
        }

    }
}