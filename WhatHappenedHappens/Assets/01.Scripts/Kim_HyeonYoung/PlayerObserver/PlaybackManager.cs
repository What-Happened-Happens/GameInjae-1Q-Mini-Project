using Assets.Script.PlayerObserver;
using Script.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Clone_Kim
{ 
    public class PlaybackManager : MonoBehaviour
    {
       private CloneManager _cloneManager;

        void Awake()
        {
            _cloneManager = GetComponent<CloneManager>();
        }

       public CloneReplay Replay(List<PlayerEvent> events)
        {
            if (events == null || events.Count == 0) return null;

            GameObject clone = _cloneManager.CreateClone(); 
            if (clone == null) return null;
            CloneReplay replayer;

            if (!clone.TryGetComponent<CloneReplay>(out replayer))
            {
                replayer = clone.AddComponent<CloneReplay>();
            }

            replayer.SetEvents(events);

            replayer.OnReplayFinished += finished =>
            {
                finished.OnReplayFinished -= null;
                Debug.Log($"클론 재생 종료");
            };
            return null;

        }


    }
}
