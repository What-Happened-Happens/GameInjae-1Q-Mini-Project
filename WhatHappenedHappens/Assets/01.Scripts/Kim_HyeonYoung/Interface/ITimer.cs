namespace Script.Interface
{
    public interface ITimer
    {
        public void TimeInit(bool CloneInput); 
        public void Update();
        public bool IsElapsed(float duration);
        public float GetDeltaTime();
        public void ResetTimer();  
    }
}