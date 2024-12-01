using System.Collections.Generic;
using SaveSystem.GameRepositories;
using SaveSystem.SaveLoaders;

namespace SaveSystem
{
    public class SaveLoadManager
    {
        private readonly IGameStateRepository _gameStateRepository;
        private readonly List<ISaveLoader> _saveLoaders;

        public SaveLoadManager(IGameStateRepository gameStateRepository, List<ISaveLoader> saveLoaders)
        {
            _gameStateRepository = gameStateRepository;
            _saveLoaders = saveLoaders;
        }

        public void SaveGame()
        {
            foreach (ISaveLoader saveLoader in _saveLoaders)
            {
                saveLoader.SaveGame();
            }

            _gameStateRepository.WriteState();
        }

        public void LoadGame()
        {
            _gameStateRepository.ReadState();

            foreach (ISaveLoader saveLoader in _saveLoaders)
            {
                saveLoader.LoadGame();
            }
        }
    }
}