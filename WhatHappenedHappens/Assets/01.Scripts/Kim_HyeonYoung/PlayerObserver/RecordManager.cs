using Assets.Script.PlayerObserver;
using Script.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace Assets.Script.Clone_Kim
{
    public class RecordManager : MonoBehaviour
    {
        public event Action OnPhaseOneFinished;
        public event Action OnPhaseTwoFinished;

        private const float RECORD_DURATION = 5f;

        private ITimer _timer;
        private PlayerSubject _subject;
        private MovementRecorder _recorderer;
        public Vector3 _recordStartPosition;

        private int _firstPauseIndex;
        private int _phase = 0; 
        private bool _isRecording = false;

        public List<PlayerEvent> RecordedEvents { get; set; }
        void Awake()
        {
            _timer = GetComponent<Timer>();
            _subject = GetComponent<PlayerSubject>();
        }

        private void Update()
        {
            if (!_isRecording) return;
            _timer.Update(); 

            if (_isRecording && _timer.IsElapsed(RECORD_DURATION))
            {
               EndRecording();
            }
        }

        public void StartPhase(int phase, int pauseIndex = 0)
        {
            _phase = phase;
            _firstPauseIndex = pauseIndex;
            _isRecording = true;
            _recordStartPosition = transform.position;
            _recorderer = new MovementRecorder();
            _subject.Attach(_recorderer);
            _timer.TimeInit(true);
        }    

        void EndRecording()
        {
            _isRecording = false;
            _subject.Detach(_recorderer); 

            if (_phase == 1)
            {
                RecordedEvents = new List<PlayerEvent>(_recorderer.Events);
                OnPhaseOneFinished?.Invoke(); 
            }
            else OnPhaseTwoFinished?.Invoke();

            transform.position = _recordStartPosition;
            Debug.Log($"Recording Finished : RecordedEventsCount : {_recorderer.Events.Count}");
        }
        public int FirstPauseIndex => _firstPauseIndex;
    }
}
