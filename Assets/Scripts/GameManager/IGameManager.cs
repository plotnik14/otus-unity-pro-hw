namespace ShootEmUp
{
    public interface IGameManager
    {
        EGameState State { get; }
        void RegisterListener(IGameLifecycleListener listener);
        void UnregisterListener(IGameLifecycleListener listener);
        void StartGame();
        void FinishGame();
        void PauseGame();
        void ResumeGame();
    }
}