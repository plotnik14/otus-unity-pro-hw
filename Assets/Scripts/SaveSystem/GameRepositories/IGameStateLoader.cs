namespace SaveSystem.GameRepositories
{
    public interface IGameStateLoader
    {
        void ReadGameState();
        void WriteGameState();
    }
}