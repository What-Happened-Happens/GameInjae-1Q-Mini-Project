using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;



// [ 패러독스 시스템의 핵심 관리 ]
public class ParadoxManager : MonoBehaviour
{
    public static ParadoxManager Instance;

    [Header("Player")]
    public GameObject player;
    public GameObject ghostPlayerPrefab;

    [Header("UI Effect")]
    public GameObject RecordEffect; // 기록 중일 때, 배경 색상 필터

    private Vector3 playerReturnPosition;

    private bool isRecording = false;
    private bool isReplaying = false;
    private int maxParadox = 3;

    // [ ★ to. 현영님 ★ ]

    // - ghostCounter 랑 recordingTimeRemaining 가져가서 UI 표시 해주시면 될 거 같아요!! 
    // Debug.Log($"현재 활성화 중인 고스트 수: {ghostCounter}");
    // Debug.Log($"[Paradox] 녹화 중... 남은 시간: {recordingTimeRemaining:F2}s");

    [Header("ParadoxTime")]
    public float recordingStartTime = 0f;
    public float replayStartTime = 0f;
    private float lastRecordTime = 0f;
    public int ghostCounter = 0;                // ★ 고스트 수 카운트 ★ -> 현영님 
    public float recordingDuration = 5f;        // [ 녹화 시간 ]
    public float recordingTimeRemaining = 0f;    // ★ 녹화 남은 시간 ★ -> 현영님 

    [Header("PlayerMovement")]
    private List<PlayerMovementRecord> currentPlayerRecording = new List<PlayerMovementRecord>();
    private Queue<List<PlayerMovementRecord>> objectQueue = new Queue<List<PlayerMovementRecord>>();

    [Header("PlayerAnimation")]
    private List<PlayerAnimationRecord> currentAnimationRecording = new List<PlayerAnimationRecord>();
    private Queue<List<PlayerAnimationRecord>> animationQueue = new Queue<List<PlayerAnimationRecord>>();


    // [ 패러독스 관려 오브젝트 위치 ]
    // SaveObjectPos()에서 저장된 위치로 돌아감
    // ResetObjectPos()에서 초기화
    [Header("Objects Position")]
    public Transform B1_Pos;
    private Vector3 B1_Start_Pos;
    public Transform B2_Pos;
    private Vector3 B2_Start_Pos;
    public Transform A_Pos;
    private Vector3 A_Start_Pos;


    // ---------------------------------------------------


    private void Awake()
    {
        // if (Instance == null) Instance = this;
        // else Destroy(gameObject);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRecording();
        }
        
        if (isRecording)
        {
            float elapsed = Time.time - recordingStartTime;
            recordingTimeRemaining = Mathf.Max(0f, recordingDuration - elapsed); // 남은 시간 계산
           

            if (elapsed - lastRecordTime >= 0.1f)
            {
                currentPlayerRecording.Add(new PlayerMovementRecord(elapsed, player.transform.position));

                foreach (var obj in FindObjectsOfType<MonoBehaviour>().OfType<IRecordable>())
                {
                    obj.RecordPosition(elapsed);
                }

                // 플레이어 애니메이션 상태 기록
                /*
                Animator animator = player.GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    string currentState = GetCurrentAnimatorState(animator);
                    currentAnimationRecording.Add(new PlayerAnimationRecord(elapsed, currentState));
                }
                */
                lastRecordTime = elapsed;
            }

            if (elapsed >= 5f)
            {
                StopRecording();
            }
        }
    }

    /*
    private string GetCurrentAnimatorState(Animator animator)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return "Idle";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            return "Walk";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            return "Jump";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            return "Attack";
        return "Unknown";
    }
    */
    // -------------------------------------------------------------------------------


    public void StartRecording()
    {
        if(!RecordEffect.activeSelf) RecordEffect.SetActive(true); 

        if (isReplaying && objectQueue.Count > 0)
        {
            float timePassed = Time.time - replayStartTime;
            Debug.Log($"[Paradox] 기존 재생 중단 및 {timePassed:F2}s 지점부터 잘라냄");
            TrimOngoingReplays(timePassed);
        }
        else if (!isReplaying && objectQueue.Count > 0)
        {
            objectQueue.Clear();
            Debug.Log("[Paradox] 기존 기록 삭제");
        }   

        Debug.Log("[Paradox] 녹화 시작");
        isRecording = true;
        recordingStartTime = Time.time;
        lastRecordTime = 0f;

        SaveObjectPos(); 

        currentPlayerRecording.Clear();

        playerReturnPosition = player.transform.position;
    }

    // [ 초기 위치 저장 ]
    public void SaveObjectPos()
    {
        B1_Start_Pos = B1_Pos.position; 
        B2_Start_Pos = B2_Pos.position;
        A_Start_Pos = A_Pos.position; 
    }

    public void StopRecording()
    {
        isRecording = false;
        if (RecordEffect.activeSelf) RecordEffect.SetActive(false);

        if (objectQueue.Count >= maxParadox)
            objectQueue.Dequeue();

        objectQueue.Enqueue(new List<PlayerMovementRecord>(currentPlayerRecording));

        Debug.Log("[Paradox] 녹화 종료");

        ResetPlayerPos();
        ResetObjectPos();
        ReplayParadoxes();
    }

    private void ResetPlayerPos()
    {
        player.transform.position = playerReturnPosition;
    }

    private void ResetObjectPos()
    {
        B1_Pos.position = B1_Start_Pos; // 플랫폼 
        B2_Pos.position = B2_Start_Pos;
        A_Pos.position = A_Start_Pos; 
    }

    private void ReplayParadoxes()
    {
        isReplaying = true;
        replayStartTime = Time.time;


        // [ 고스트 관련 ]
        ghostCounter = 0;

        var queueArray = objectQueue.ToArray();
        for (int i = 0; i < queueArray.Length; i++)
        {
            var playerRecords = queueArray[i];      //여개;;;;;;;
            
            if (playerRecords == null || playerRecords.Count < 2)
            {
                // Debug.LogWarning($"[Paradox] 고스트 {i} 데이터 부족");
                continue;
            }

            // 고스트 생성 및 재생
            GameObject ghost = Instantiate(ghostPlayerPrefab);
            ghost.name = "GhostPlayer_" + i;
            ghost.transform.position = playerRecords[0].position;

            // TimerText 찾아서 넘기기
            TextMeshPro ghostText = ghost.transform.Find("TimerText")?.GetComponent<TextMeshPro>();

            ghostCounter++;
            
            StartCoroutine(ReplayGhostMovement(ghost, playerRecords, ghostText));
        }
    }

    private IEnumerator ReplayGhostMovement(GameObject ghost, List<PlayerMovementRecord> data, TextMeshPro timerText)
    {
        SpriteRenderer sr = ghost.GetComponentInChildren<SpriteRenderer>(); // 자식 포함

        // 전체 고스트 재생 시간 계산
        float totalDuration = data[data.Count - 1].time - data[0].time;
        float elapsedTotal = 0f;

        for (int i = 1; i < data.Count; i++)
        {
            float waitTime = data[i].time - data[i - 1].time;
            Vector3 start = data[i - 1].position;
            Vector3 end = data[i].position;

            // 좌우 반전 
            if (sr != null)
            {
                if (end.x < start.x)        sr.flipX = true;
                else if (end.x > start.x)   sr.flipX = false;
            }

            float elapsed = 0f;
            while (elapsed < waitTime)
            {
                if (ghost == null) yield break;

                ghost.transform.position = Vector3.Lerp(start, end, elapsed / waitTime);

                elapsed += Time.deltaTime;
                elapsedTotal += Time.deltaTime;

                // 남은 시간 출력
                int remainingTime = (int)Mathf.Max(0f, totalDuration - elapsedTotal);
                if (timerText != null)
                {
                    timerText.text = remainingTime.ToString("D2");
                }

                yield return null;
            }

            if (ghost != null)
                ghost.transform.position = end;
        }

        if (ghost != null)
            Destroy(ghost);

        ghostCounter--;
        if (ghostCounter == 0) ResetAfterReplay();
    }


    private void ResetAfterReplay()
    {
        isReplaying = false;
        ResetObjectPos();
    }

    // [ 패러독스 자르기 ]
    public void TrimOngoingReplays(float timePassed)
    {
        var trimmedQueue = new Queue<List<PlayerMovementRecord>>();

        foreach (var playerRecords in objectQueue)
        {
            var trimmedPlayer = playerRecords
                .Where(r => r.time >= timePassed)
                .Select(r => new PlayerMovementRecord(r.time - timePassed, r.position))
                .ToList();

            trimmedQueue.Enqueue(trimmedPlayer);
        }

        objectQueue = trimmedQueue;
    }
}