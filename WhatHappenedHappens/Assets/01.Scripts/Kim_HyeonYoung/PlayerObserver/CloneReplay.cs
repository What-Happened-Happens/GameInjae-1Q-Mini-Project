using System.Collections.Generic;
using UnityEngine;
using Script.Interface;
using System;

namespace Assets.Script.PlayerObserver
{
    public class CloneReplay : MonoBehaviour
    {
        public event Action<CloneReplay> OnReplayFinished;

        List<PlayerEvent> _events;
        int _next = 0;
        bool _paused = false;
        public void Pause() => _paused = true;
        public void Resume() => _paused = false;
        public int NextIndex
        {
            get => _next;
            set => _next = value;
        }

        public void SetEvents(List<PlayerEvent> recorderded)
        {
            _events = new List<PlayerEvent>(recorderded); 
            _next = 0;
            _paused = false;
            gameObject.SetActive(true); 
        }

        public void PauseAndHide() 
        {
            _paused = true; 
            gameObject.SetActive(false); 
        }
        public void ResumeAndShow()
        {
            _paused = false;
            gameObject.SetActive(true);
        }

        public void Update()
        {
            if (_paused || _events == null) return;
            if (_next < _events.Count)
            {
                var Event = _events[_next++];
                transform.position = Event.position;
                if (Event.type == PlayerEventType.MOVE || Event.type == PlayerEventType.JUMP) { transform.position = Event.position; }
            }
            else
            {
                OnReplayFinished?.Invoke(this);
                Destroy(gameObject); 
            }
            
        }
       
    }
}
