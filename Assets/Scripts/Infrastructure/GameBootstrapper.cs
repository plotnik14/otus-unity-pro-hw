using GameEngine;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private UnitManager _unitManager;
        private ResourceService _resourceService;

        [UsedImplicitly]
        private void Awake()
        {
            _unitManager.SetupUnits(FindObjectsOfType<Unit>());
            _resourceService.SetResources(FindObjectsOfType<Resource>());
        }

        [Inject]
        public void Construct(UnitManager unitManager, ResourceService resourceService)
        {
            _unitManager = unitManager;
            _resourceService = resourceService;
        }
    }
}