using System.Collections.Generic;
using Script.Interface;
using Script.Interface.Assets.Script.PlayerObserver;

namespace Assets.Script.PlayerObserver
{
    public class MovementRecorder : IPlayerObserver
    {
        public List<PlayerEvent> Events { get; } = new List<PlayerEvent>(); 
        public void OnNotify(in PlayerEvent Event) { Events.Add(Event); } 
    }
}
