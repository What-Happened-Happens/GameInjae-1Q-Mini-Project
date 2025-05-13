using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [ 패러독스 시스템의 핵심 관리 ]
public class ParadoxManager : MonoBehaviour
{
    public static ParadoxManager Instance;

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

    // PlayerMovementRecord + IRecordable 묶은 전체 상태 기록 큐
    private Queue<(List<PlayerMovementRecord> playerRecords, List<List<ObjectMovementRecord>> objectRecords)> objectQueue
        = new Queue<(List<PlayerMovementRecord>, List<List<ObjectMovementRecord>>)>();

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

            if (elapsed >= 10f)
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

        currentPlayerRecording.Clear();

        // ResetScene();
        ClearAllObjectRecords();

        playerReturnPosition = player.transform.position;
    }

    private void ClearAllObjectRecords()
    {
        foreach (var obj in FindObjectsOfType<MonoBehaviour>().OfType<IRecordable>())
        {
            obj.SetMovementRecords(new List<ObjectMovementRecord>());
        }
    }

    public void StopRecording()
    {
        isRecording = false;

        if (objectQueue.Count >= maxParadox)
            objectQueue.Dequeue();

        objectQueue.Enqueue((new List<PlayerMovementRecord>(currentPlayerRecording), GetAllObjectRecords()));

        Debug.Log("[Paradox] 녹화 종료");

        ResetScene();
        ReplayParadoxes();
    }

    private void ResetScene()
    {
        player.transform.position = playerReturnPosition;
        foreach (var box in FindObjectsOfType<Box>()) box.ResetPosition();
        foreach (var ball in FindObjectsOfType<Ball>()) ball.ResetPosition();
    }

    private List<List<ObjectMovementRecord>> GetAllObjectRecords()
    {
        List<List<ObjectMovementRecord>> result = new List<List<ObjectMovementRecord>>();
        foreach (var obj in FindObjectsOfType<MonoBehaviour>().OfType<IRecordable>())
        {
            result.Add(obj.GetMovementRecords());
        }
        return result;
    }

    private void ReplayParadoxes()
    {
        isReplaying = true;
        replayStartTime = Time.time;

        ghostCounter = 0;

        var queueArray = objectQueue.ToArray();
        for (int i = 0; i < queueArray.Length; i++)
        {
            var (playerRecords, objectRecords) = queueArray[i];

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

            // 실제 오브젝트 위치 재생
            var recordables = FindObjectsOfType<MonoBehaviour>().OfType<IRecordable>().ToList();
            for (int j = 0; j < Mathf.Min(objectRecords.Count, recordables.Count); j++)
            {
                recordables[j].SetMovementRecords(objectRecords[j]);
                StartCoroutine(recordables[j].ReplayMovement());
            }
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
        if (ghostCounter == 0)
            ResetObjectsAfterPlayback();
    }


    private void ResetObjectsAfterPlayback()
    {
        foreach (var box in FindObjectsOfType<Box>()) box.ResetPosition();
        foreach (var ball in FindObjectsOfType<Ball>()) ball.ResetPosition();

        isReplaying = false;
    }

    public void TrimOngoingReplays(float timePassed)
    {
        var trimmedQueue = new Queue<(List<PlayerMovementRecord>, List<List<ObjectMovementRecord>>)>();

        foreach (var (playerRecords, objectRecords) in objectQueue)
        {
            var trimmedPlayer = playerRecords
                .Where(r => r.time >= timePassed)
                .Select(r => new PlayerMovementRecord(r.time - timePassed, r.position))
                .ToList();

            var trimmedObjects = objectRecords
                .Select(list => list
                    .Where(r => r.time >= timePassed)
                    .Select(r => new ObjectMovementRecord(r.time - timePassed, r.position))
                    .ToList()
                ).ToList();

            trimmedQueue.Enqueue((trimmedPlayer, trimmedObjects));
        }

        objectQueue = trimmedQueue;
    }
}