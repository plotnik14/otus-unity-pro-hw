namespace ShootEmUp
{
    public interface IGameFixedUpdateListener : IGameLifecycleListener
    {
        void OnGameFixedUpdate(float deltaTime);
    }
}