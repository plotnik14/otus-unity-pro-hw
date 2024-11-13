using GameEngine;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _unitsContainer;

        public override void InstallBindings()
        {
            UnitManager unitManager = new(_unitsContainer);
            unitManager.SetupUnits(FindObjectsOfType<Unit>());
            Container.BindInstance(unitManager).AsSingle();

            ResourceService resourceService = new();
            resourceService.SetResources(FindObjectsOfType<Resource>());
            Container.BindInstance(resourceService).AsSingle();
        }
    }
}