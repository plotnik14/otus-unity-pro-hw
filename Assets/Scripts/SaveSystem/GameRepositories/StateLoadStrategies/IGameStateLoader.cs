using System.Collections.Generic;

namespace SaveSystem.GameRepositories.StateLoadStrategies
{
    public interface IGameStateLoader
    {
        Dictionary<string, string> ReadState(string key);
        void WriteState(string key, Dictionary<string, string> data);
    }
}