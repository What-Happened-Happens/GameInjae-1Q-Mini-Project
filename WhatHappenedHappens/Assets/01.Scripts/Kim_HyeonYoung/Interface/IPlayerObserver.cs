using UnityEngine;
namespace Script.Interface
{
    public enum PlayerEventType { MOVE, JUMP, INTERACT }

    public struct PlayerEvent
    {
        public PlayerEventType type;
        public Vector3 position;
        public float direction;
        public float deltaTime;
    }
    namespace Assets.Script.PlayerObserver
    {
        public interface IPlayerObserver
        {
            void OnNotify(in PlayerEvent Event);
        }
    }
}
