namespace ShootEmUp
{
    public interface IGameFinishListener : IGameLifecycleListener
    {
        void OnGameFinish();
    }
}