using GameEngine;
using SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Helpers
{
    public class DataHelper : MonoBehaviour
    {
        [ShowInInspector]
        private UnitManager _unitManager;

        [ShowInInspector, ReadOnly]
        private ResourceService _resourceService;

        private SaveLoadManager _saveLoadManager;

        [Inject]
        public void Construct(
            UnitManager unitManager,
            ResourceService resourceService,
            SaveLoadManager saveLoadManager)
        {
            _unitManager = unitManager;
            _resourceService = resourceService;
            _saveLoadManager = saveLoadManager;
        }

        [Button("Загрузить файл")]
        public void Load() => _saveLoadManager.LoadGame();

        [Button("Сохранить в файл")]
        public void Save() => _saveLoadManager.SaveGame();
    }
}