namespace SaveSystem.GameRepositories
{
    public interface IGameStateRepository
    {
        bool TryGetData<T>(out T data);
        void SetData<T>(T data);
    }
}