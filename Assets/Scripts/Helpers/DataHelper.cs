using GameEngine;
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

        [Inject]
        public void Construct(UnitManager unitManager, ResourceService resourceService)
        {
            _unitManager = unitManager;
            _resourceService = resourceService;
        }

        [Button("Загрузить файл")]
        public void Load()
        {
            // TODO
        }

        [Button("Сохранить в файл")]
        public void Save()
        {
            // TODO
        }
    }
}