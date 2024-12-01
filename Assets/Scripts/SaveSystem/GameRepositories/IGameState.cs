using System.Collections.Generic;

namespace SaveSystem.GameRepositories
{
    public interface IGameState
    {
        string Key { get; }
        Dictionary<string, string> Data { get; set; }
    }
}