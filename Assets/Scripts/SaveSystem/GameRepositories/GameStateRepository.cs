using System.Collections.Generic;
using Newtonsoft.Json;
using SaveSystem.GameRepositories.StateLoadStrategies;

namespace SaveSystem.GameRepositories
{
    public class GameStateRepository : IGameStateRepository
    {
        private const string GAME_STATE_KEY = "GameStateKey";

        private readonly IGameStateLoader _gameStateLoader;
        private Dictionary<string, string> _gameState = new();

        public GameStateRepository(IGameStateLoader _gameStateLoader)
        {
            this._gameStateLoader = _gameStateLoader;
        }

        public bool TryGetData<T>(out T data)
        {
            string key = typeof(T).ToString();

            if (_gameState.TryGetValue(key, out var jsonData))
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
            _gameState[key] = jsonData;
        }

        public void ReadState() => _gameState = _gameStateLoader.ReadState(GAME_STATE_KEY);

        public void WriteState() => _gameStateLoader.WriteState(GAME_STATE_KEY, _gameState);
    }
}