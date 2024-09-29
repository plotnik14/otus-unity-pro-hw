namespace ShootEmUp
{
    public interface IGameUpdateListener : IGameLifecycleListener
    {
        void OnGameUpdate(float deltaTime);
    }
}