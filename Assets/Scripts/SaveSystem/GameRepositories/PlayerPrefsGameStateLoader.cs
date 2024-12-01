using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem.GameRepositories
{
    public class PlayerPrefsGameStateLoader : IGameStateLoader
    {
        private readonly IGameState _gameState;

        public PlayerPrefsGameStateLoader(IGameState gameState)
        {
            _gameState = gameState;
        }

        public void ReadGameState()
        {
            if (PlayerPrefs.HasKey(_gameState.Key))
            {
                var gameStateJson = PlayerPrefs.GetString(_gameState.Key);
                _gameState.Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(gameStateJson);
                return;
            }

            Debug.LogWarning("State was not loaded");
            _gameState.Data = new Dictionary<string, string>();
        }

        public void WriteGameState()
        {
            var gameStateJson = JsonConvert.SerializeObject(_gameState.Data);
            PlayerPrefs.SetString(_gameState.Key, gameStateJson);
        }
    }
}