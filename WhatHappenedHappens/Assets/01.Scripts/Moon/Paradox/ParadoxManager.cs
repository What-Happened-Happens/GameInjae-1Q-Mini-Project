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

    [Header("UI")]
    public GameObject RecordEffect;

    [Header("Paradox Settings")]
    public int maxParadox = 3;
    public float recordingDuration = 15f;

    public int ghostCounter = 0;
    public float recordingTimeRemaining = 0f;

    public bool isRecording = false;
    public bool isReplaying = false;

    private float recordingStartTime = 0f;
    private float replayStartTime = 0f;

    private ParadoxRecorder recorder;
    private ParadoxGhostPlayer ghostPlayer;
    public ParadoxObjectManager objectManager;

    private void Awake()
    {
        // if (Instance == null) Instance = this;
        // else Destroy(gameObject);

        recorder = new ParadoxRecorder();
        ghostPlayer = new ParadoxGhostPlayer();
        // positionManager = new ParadoxPositionManager();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRecording();
            SoundManager.Instance.PlaySFX("Paradox_record", 1.0f);
        }

        if (isRecording)
        {
            float elapsed = Time.time - recordingStartTime;
            recordingTimeRemaining = Mathf.Max(0f, recordingDuration - elapsed);

            recorder.Record(player, elapsed, objectManager.triggerObjects);

            if (elapsed >= recordingDuration)
                StopRecording();
        }
    }

    public void StartRecording()
    {
        if (!RecordEffect.activeSelf) RecordEffect.SetActive(true);

        if (isReplaying)
        {
            float trimTime = Time.time - replayStartTime;
            recorder.Trim(trimTime);
        }
        else
        {
            recorder.Clear();
        }

        isRecording = true;
        recordingStartTime = Time.time;

        objectManager.Save(); // 오브젝트 위치 및 트리거 저장
        objectManager.SavePlayer(player); // 플레이어 위치 저장
        recorder.Start();
    }

    public void StopRecording()
    {
        isRecording = false;
        RecordEffect.SetActive(false);

        recorder.Enqueue(maxParadox);
        objectManager.ResetPlayer(player);
        objectManager.ResetAll();

        StartReplay();
    }

    private void StartReplay()
    {
        isReplaying = true;
        replayStartTime = Time.time;
        ghostCounter = 0;

        List<List<PlayerMovementRecord>> moveData = recorder.GetAllMovementData();
        List<List<PlayerAnimationRecord>> animData = recorder.GetAllAnimationData();
        List<Dictionary<GameObject, List<ObjectTrueFalseRecord>>> tfData = recorder.GetAllTrueFalseData();


        for (int i = 0; i < moveData.Count; i++)
        {
            var ghost = Instantiate(ghostPlayerPrefab);
            ghost.name = $"GhostPlayer_{i}";
            ghost.transform.position = moveData[i][0].position;

            var timerText = ghost.transform.Find("TimerText")?.GetComponent<TextMeshPro>();
            ghostCounter++;

            StartCoroutine(ghostPlayer.Replay(ghost, moveData[i], animData[i], timerText, () =>
            {
                ghostCounter--;
                if (ghostCounter == 0) EndReplay();
            }));
        }

        for (int i = 0; i < tfData.Count; i++)
        {
            var tfDict = tfData[i];
            foreach (var kvp in tfDict)
            {
                var obj = kvp.Key;
                var records = kvp.Value;

                StartCoroutine(ReplayTrueFalse(obj, records));
            }
        }
    }

    private IEnumerator ReplayTrueFalse(GameObject obj, List<ObjectTrueFalseRecord> records)
    {
        foreach (var record in records)
        {
            yield return new WaitForSeconds(record.time);
            var tf = obj.GetComponent<TrueFalse>();
            if (tf != null) tf.SetState(record.state);
        }
    }

    private void EndReplay()
    {
        isReplaying = false;
        objectManager.ResetAll();
    }
}