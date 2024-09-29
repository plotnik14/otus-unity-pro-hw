namespace ShootEmUp
{
    public interface IGameStartListener : IGameLifecycleListener
    {
        void OnGameStart();
    }
}