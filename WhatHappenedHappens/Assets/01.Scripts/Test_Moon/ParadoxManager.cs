using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [ �з����� �ý����� �ٽ� ���� ]
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

    // [ ������Ʈ �ʱ� ��ġ ]
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
            Debug.Log($"[Paradox] ���� ��� �ߴ� �� {timePassed:F2}s �������� �߶�");
            TrimOngoingReplays(timePassed);
        }

        Debug.Log("[Paradox] ��ȭ ����");
        isRecording = true;
        recordingStartTime = Time.time;
        lastRecordTime = 0f;

        SaveStartPoint();

        currentPlayerRecording.Clear();

        playerReturnPosition = player.transform.position;
    }

    // [ �ʱ� ��ġ ���� ]
    public void SaveStartPoint()
    {
        B1_Start_Pos = B1_Pos.position; // �÷��� 
        B2_Start_Pos = B2_Pos.position; 

        Debug.Log($"[Paradox] �ʱ� ��ġ ����: B1({B1_Start_Pos}), B2({B2_Start_Pos})");
    }

    public void StopRecording()
    {
        isRecording = false;

        if (objectQueue.Count >= maxParadox)
            objectQueue.Dequeue();

        objectQueue.Enqueue(new List<PlayerMovementRecord>(currentPlayerRecording));

        Debug.Log("[Paradox] ��ȭ ����");

        ResetScene();
        ReplayParadoxes();
    }

    private void ResetScene()
    {
        B1_Pos.position = B1_Start_Pos;
        B2_Pos.position = B2_Start_Pos;

        Debug.Log($"[Paradox] �ʱ� ��ġ ����: B1({B1_Start_Pos}), B2({B2_Start_Pos})");

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
                Debug.LogWarning($"[Paradox] ��Ʈ {i} ������ ����");
                continue;
            }

            // ��Ʈ ���� �� ���
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