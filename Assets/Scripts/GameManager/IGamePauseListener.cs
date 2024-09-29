namespace ShootEmUp
{
    public interface IGamePauseListener : IGameLifecycleListener
    {
        void OnGamePause();
    }
}