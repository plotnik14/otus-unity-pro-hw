using System.Collections.Generic;
using Newtonsoft.Json;

namespace SaveSystem.GameRepositories
{
    public class GameStateRepository : IGameStateRepository, IGameState
    {
        public string Key => "GameStateKey";
        public Dictionary<string, string> Data { get; set; } = new();

        public bool TryGetData<T>(out T data)
        {
            string key = typeof(T).ToString();

            if (Data.TryGetValue(key, out var jsonData))
            {
                data = JsonConvert.DeserializeObject<T>(jsonData);
                return true;
            }

            data = default;
            return false;
        }

        public void SetData<T>(T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var key = typeof(T).ToString();
            Data[key] = jsonData;
        }
    }
}