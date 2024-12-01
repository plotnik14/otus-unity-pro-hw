using System.Collections.Generic;
using SaveSystem.GameRepositories;
using SaveSystem.SaveLoaders;

namespace SaveSystem
{
    public class SaveLoadManager
    {
        private readonly IGameStateLoader _gameStateLoader;
        private readonly List<ISaveLoader> _saveLoaders;

        public SaveLoadManager(IGameStateLoader gameStateLoader, List<ISaveLoader> saveLoaders)
        {
            _gameStateLoader = gameStateLoader;
            _saveLoaders = saveLoaders;
        }

        public void SaveGame()
        {
            foreach (ISaveLoader saveLoader in _saveLoaders)
            {
                saveLoader.SaveGame();
            }

            _gameStateLoader.WriteGameState();
        }

        public void LoadGame()
        {
            _gameStateLoader.ReadGameState();

            foreach (ISaveLoader saveLoader in _saveLoaders)
            {
                saveLoader.LoadGame();
            }
        }
    }
}