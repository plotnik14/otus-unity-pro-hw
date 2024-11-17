using System.Collections.Generic;
using SaveSystem.GameRepositories;
using SaveSystem.SaveLoaders;

namespace SaveSystem
{
    public class SaveLoadManager
    {
        private readonly IGameRepository _gameRepository;
        private readonly List<ISaveLoader> _saveLoaders;

        public SaveLoadManager(IGameRepository gameRepository, List<ISaveLoader> saveLoaders)
        {
            _gameRepository = gameRepository;
            _saveLoaders = saveLoaders;
        }

        public void SaveGame()
        {
            foreach (ISaveLoader saveLoader in _saveLoaders)
            {
                saveLoader.SaveGame();
            }

            _gameRepository.SaveState();
        }

        public void LoadGame()
        {
            _gameRepository.LoadState();

            foreach (ISaveLoader saveLoader in _saveLoaders)
            {
                saveLoader.LoadGame();
            }
        }
    }
}