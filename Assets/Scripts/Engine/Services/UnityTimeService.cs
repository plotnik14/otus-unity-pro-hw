namespace Engine.Services
{
    public class UnityTimeService : ITimeService
    {
        public float DeltaTime => UnityEngine.Time.deltaTime;

        public float Time =>  UnityEngine.Time.time;

        public void Pause() => UnityEngine.Time.timeScale = 0;

        public void Resume() => UnityEngine.Time.timeScale = 1;
    }
}