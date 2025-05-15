using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [ 패러독스 시스템의 핵심 관리 ]
public class ParadoxManager : MonoBehaviour
{
    public static ParadoxManager Instance;

    [Header("Player")]
    public GameObject player;
    public GameObject ghostPlayerPrefab;

    private Vector3 playerReturnPosition;

    private bool isRecording = false;
    private bool isReplaying = false;
    private int maxParadox = 3;

    public float recordingStartTime = 0f;
    public float replayStartTime = 0f;
    private float lastRecordTime = 0f;
    private int ghostCounter = 0;

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

    public void StartRecording()
    {
        if (isReplaying && objectQueue.Count > 0)
        {
            float timePassed = Time.time - replayStartTime;
            Debug.Log($"[Paradox] 기존 재생 중단 및 {timePassed:F2}s 지점부터 잘라냄");
            TrimOngoingReplays(timePassed);
        }

        Debug.Log("[Paradox] 녹화 시작");
        isRecording = true;
        recordingStartTime = Time.time;
        lastRecordTime = 0f;

        SaveStartPoint();

        currentPlayerRecording.Clear();

        playerReturnPosition = player.transform.position;
    }

    // [ 초기 위치 저장 ]
    public void SaveStartPoint()
    {
        B1_Start_Pos = B1_Pos.position; // 플랫폼 
        B2_Start_Pos = B2_Pos.position; 

        Debug.Log($"[Paradox] 초기 위치 저장: B1({B1_Start_Pos}), B2({B2_Start_Pos})");
    }

    public void StopRecording()
    {
        isRecording = false;

        if (objectQueue.Count >= maxParadox)
            objectQueue.Dequeue();

        objectQueue.Enqueue(new List<PlayerMovementRecord>(currentPlayerRecording));

        Debug.Log("[Paradox] 녹화 종료");

        ResetScene();
        ReplayParadoxes();
    }

    private void ResetScene()
    {
        B1_Pos.position = B1_Start_Pos;
        B2_Pos.position = B2_Start_Pos;

        Debug.Log($"[Paradox] 초기 위치 복원: B1({B1_Start_Pos}), B2({B2_Start_Pos})");

        player.transform.position = playerReturnPosition;
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
                Debug.LogWarning($"[Paradox] 고스트 {i} 데이터 부족");
                continue;
            }

            // 고스트 생성 및 재생
            GameObject ghost = Instantiate(ghostPlayerPrefab);
            ghost.name = "GhostPlayer_" + i;
            ghost.transform.position = playerRecords[0].position;

            ghostCounter++;
            StartCoroutine(ReplayGhostMovement(ghost, playerRecords));
        }
    }

    private IEnumerator ReplayGhostMovement(GameObject ghost, List<PlayerMovementRecord> data)
    {
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
                yield return null;
            }

            if (ghost != null)
                ghost.transform.position = end;
        }

        if (ghost != null)
            Destroy(ghost);

        ghostCounter--;
        if (ghostCounter == 0) ResetObjectsAfterPlayback();
    }


    private void ResetObjectsAfterPlayback()
    {
        isReplaying = false;
    }

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