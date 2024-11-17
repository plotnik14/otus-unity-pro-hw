using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem.GameRepositories
{
    public class FileRepository : IGameRepository
    {
        private const string GAME_STATE_KEY = "GameStateKey";

        private Dictionary<string, string> _gameState = new();

        public bool TryGetData<T>(out T data)
        {
            var key = typeof(T).ToString();

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

        public void LoadState()
        {
            if (!ES3.KeyExists(GAME_STATE_KEY))
            {
                Debug.LogError("State was not loaded");
                return;
            }

            _gameState = ES3.Load(GAME_STATE_KEY, new Dictionary<string, string>());
        }

        public void SaveState()
        {
            ES3.Save(GAME_STATE_KEY, _gameState);
        }
    }
}