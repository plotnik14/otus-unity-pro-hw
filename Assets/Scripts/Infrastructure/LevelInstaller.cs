using GameEngine;
using SaveSystem;
using SaveSystem.GameRepositories;
using SaveSystem.GameRepositories.StateLoadStrategies;
using SaveSystem.SaveLoaders;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _unitsContainer;

        public override void InstallBindings()
        {
            BindUnitManager();
            BindResourceService();
            BindRepository();
            BindSaveLoaders();
            Container.Bind<SaveLoadManager>().AsSingle();
        }

        private void BindUnitManager()
        {
            UnitManager unitManager = new(_unitsContainer);
            unitManager.SetupUnits(FindObjectsOfType<Unit>());
            Container.BindInstance(unitManager).AsSingle();
        }

        private void BindResourceService()
        {
            ResourceService resourceService = new();
            resourceService.SetResources(FindObjectsOfType<Resource>());
            Container.BindInstance(resourceService).AsSingle();
        }

        private void BindRepository()
        {
            Container.BindInterfacesTo<FileGameStateLoader>().AsSingle();
            // Container.BindInterfacesTo<PlayerPrefsGameStateLoader>().AsSingle();
            Container.BindInterfacesTo<GameStateRepository>().AsSingle();
        }

        private void BindSaveLoaders()
        {
            Container.BindInterfacesTo<ResourcesSaveLoader>().AsSingle();
            Container.BindInterfacesTo<UnitsSaveLoader>().AsSingle();
        }
    }
}