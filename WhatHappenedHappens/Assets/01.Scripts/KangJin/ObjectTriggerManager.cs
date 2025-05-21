using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectTriggerManager : MonoBehaviour
{
    public static ObjectTriggerManager instance;

    [Header("Objects")]
    public List<TrueFalse> triggers;

    [Header("Player")]
    public GameObject player;
    public GameObject ghostPlayerPrefab;

    [Header("UI")]
    public GameObject RecordEffect;

    [Header("Paradox Settings")]
    public int maxParadox = 3;
    public float recordingDuration = 5f;

    public int ghostCounter = 0;
    public float recordingTimeRemaining = 0f;

    private bool isRecording = false;
    public bool recording { get { return isRecording; } }
    private bool isReplaying = false;
    public bool Replaying { get { return isReplaying; } }
    private float recordingStartTime = 0f;
    private float replayStartTime = 0f;

    private ParadoxRecorder recorder;
    private ParadoxGhostPlayer  ghostPlayer;
    public  ParadoxObjectManager objectManager;
    public  List<ParadoxTriggerRecorder> triggerRecorder;

    private void Awake()
    {
        // if (Instance == null) Instance = this;
        // else Destroy(gameObject);

        recorder = new ParadoxRecorder();
        ghostPlayer = new ParadoxGhostPlayer();
        triggerRecorder = new List<ParadoxTriggerRecorder>();
        triggerRecorder.Clear();
        // positionManager = new ParadoxPositionManager();
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
            recordingTimeRemaining = Mathf.Max(0f, recordingDuration - elapsed);

            recorder.Record(player, elapsed);
            

            if (elapsed >= recordingDuration)
                StopRecording();
        }
    }

    public void StartRecording()
    {
        if (!RecordEffect.activeSelf) RecordEffect.SetActive(true);

        if (isReplaying)
        {
            //잘라내는 시간 계산
            float trimTime = Time.time - replayStartTime;
            recorder.Trim(trimTime); // 
            /*triggerRecorder.Trim(trimTime);*/
        }
        else
        {
            recorder.Clear();
            /*triggerRecorder.Clear();*/
        }

        isRecording = true;
        recordingStartTime = Time.time;

        objectManager.Save(); // 오브젝트 위치 저장
        objectManager.SavePlayer(player); // 플레이어 위치 저장
        recorder.Start();
        /*triggerRecorder.Start();*/
    }

    public void StopRecording()
    {
        isRecording = false;
        RecordEffect.SetActive(false);

        recorder.Enqueue(maxParadox);
        /*triggerRecorder.Enqueue(maxParadox);*/
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
        /*List<List<ObjectTriggerRecord>> triggerData = triggerRecorder.GetAllTriggerData();*/

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
    }

    private void EndReplay()
    {
        isReplaying = false;
        objectManager.ResetAll();
    }
}
