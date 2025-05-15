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

    [Header("ParadoxTime")]
    public float recordingStartTime = 0f;
    public float replayStartTime = 0f;
    private float lastRecordTime = 0f;
    private int ghostCounter = 0;

    [Header("PlayerMovement")]
    private List<PlayerMovementRecord> currentPlayerRecording = new List<PlayerMovementRecord>();
    private Queue<List<PlayerMovementRecord>> objectQueue = new Queue<List<PlayerMovementRecord>>();


    // [ 오브젝트 초기 위치 ]
    [Header("Objects Position")]
    public Transform B1_Pos;
    private Vector3 B1_Start_Pos;
    public Transform B2_Pos;
    private Vector3 B2_Start_Pos;



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        // ★ 현영님! UI에서 ghostCounter 값 가져가서 표시 해주시면 될 거 같아요!! 
        // Debug.Log($"현재 활성화 중인 고스트 수: {ghostCounter}");

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRecording();
        }
        
        if (isRecording)
        {
            float elapsed = Time.time - recordingStartTime;

            if (elapsed - lastRecordTime >= 0.1f)
            {
                currentPlayerRecording.Add(new PlayerMovementRecord(elapsed, player.transform.position));

                foreach (var obj in FindObjectsOfType<MonoBehaviour>().OfType<IRecordable>())
                {
                    obj.RecordPosition(elapsed);
                }

                lastRecordTime = elapsed;
            }

            if (elapsed >= 5f)
            {
                StopRecording();
            }
        }
    }


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

        SaveObjectPos(); // 각 녹화 때의 위치로 돌아가는건지 ? -> 맞음!

        currentPlayerRecording.Clear();

        playerReturnPosition = player.transform.position;
    }

    // [ 초기 위치 저장 ]
    public void SaveObjectPos()
    {
        B1_Start_Pos = B1_Pos.position; // 플랫폼 
        B2_Start_Pos = B2_Pos.position; 
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
    }

    private void ReplayParadoxes()
    {
        isReplaying = true;
        replayStartTime = Time.time;

        ghostCounter = 0;

        var queueArray = objectQueue.ToArray();
        for (int i = 0; i < queueArray.Length; i++)
        {
            var playerRecords = queueArray[i];

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
        // 전체 고스트 재생 시간 계산
        float totalDuration = data[data.Count - 1].time - data[0].time;
        float elapsedTotal = 0f;

        for (int i = 1; i < data.Count; i++)
        {
            float waitTime = data[i].time - data[i - 1].time;
            Vector3 start = data[i - 1].position;
            Vector3 end = data[i].position;

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