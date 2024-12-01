using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.GameRepositories
{
    public class FileGameStateLoader : IGameStateLoader
    {
        private readonly IGameState _gameState;

        public FileGameStateLoader(IGameState gameState)
        {
            _gameState = gameState;
        }

        public void ReadGameState()
        {
            if (ES3.KeyExists(_gameState.Key))
            {
                _gameState.Data = ES3.Load(_gameState.Key, new Dictionary<string, string>());
                return;
            }

            Debug.LogError("State was not loaded");
            _gameState.Data = new Dictionary<string, string>();
        }

        public void WriteGameState()
        {
            ES3.Save(_gameState.Key, _gameState.Data);
        }
    }
}