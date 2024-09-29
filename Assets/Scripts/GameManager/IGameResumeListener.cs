namespace ShootEmUp
{
    public interface IGameResumeListener : IGameLifecycleListener
    {
        void OnGameResume();
    }
}