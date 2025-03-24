using System.Collections.Generic;
using Configs;
using Core;
using Core.Systems;
using GameState;
using GameState.Systems;
using Services;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UnitConfig _unitConfig;
        [SerializeField] private ProjectileConfig _projectileConfig;
        [SerializeField] private SpawnArmyConfig _spawnArmyConfig;

        [SerializeField] private UiArmyCountView _redUiArmyCountView;
        [SerializeField] private UiArmyCountView _blueUiArmyCountView;

        public override void InstallBindings()
        {
            BindConfiguration();
            BindServices();
            BindViews();
            BindContexts();
            BindSystems();
            Container.Bind<SystemProvider>().AsSingle();
            Container.Bind<GameController>().AsSingle();
        }

        private void BindConfiguration()
        {
            Container.BindInstance(_unitConfig).AsSingle();
            Container.BindInstance(_projectileConfig).AsSingle();
            Container.BindInstance(_spawnArmyConfig).AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<AssetLoader>().AsSingle();
            Container.BindInterfacesTo<UnityTimeService>().AsSingle();
            Container.BindInterfacesTo<GameObjectFactory>().AsSingle();
        }

        private void BindViews()
        {
            Container
                .BindInstance(new List<UiArmyCountView> { _redUiArmyCountView, _blueUiArmyCountView })
                .AsSingle();
        }

        private void BindContexts()
        {
            Contexts contexts = Contexts.sharedInstance;
            Container.BindInstance(contexts).AsSingle();
            Container.BindInterfacesAndSelfTo<GameContext>().FromInstance(contexts.game).AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateContext>().FromInstance(contexts.gameState).AsSingle();
        }

        private void BindSystems()
        {
            Container.Bind<SpawnArmySystem>().AsSingle();
            Container.Bind<MovementSystem>().AsSingle();
            Container.Bind<RotationSystem>().AsSingle();
            Container.Bind<UnitCollisionSystem>().AsSingle();
            Container.Bind<ProjectileCollisionSystem>().AsSingle();
            Container.Bind<DealDamageSystem>().AsSingle();
            Container.Bind<ReleaseTargetSystem>().AsSingle();
            Container.Bind<UnitDieSystem>().AsSingle();
            Container.Bind<FindTargetSystem>().AsSingle();
            Container.Bind<AttackCooldownSystem>().AsSingle();
            Container.Bind<AttackSystem>().AsSingle();
            Container.Bind<LookAtTargetSystem>().AsSingle();
            Container.Bind<SpawnProjectileSystem>().AsSingle();
            Container.Bind<AddAttackCooldownSystem>().AsSingle();
            Container.Bind<LifeTimeSystem>().AsSingle();
            Container.Bind<MultiCreateViewSystem>().AsSingle();
            Container.Bind<GameDestroyedEventSystem>().AsSingle();
            Container.Bind<PositionEventSystem>().AsSingle();
            Container.Bind<RotationEventSystem>().AsSingle();
            Container.Bind<TeamEventSystem>().AsSingle();
            Container.Bind<AttackRequestCleanup>().AsSingle();
            Container.Bind<DieRequestCleanup>().AsSingle();
            Container.Bind<InitArmyCountersSystem>().AsSingle();
            Container.Bind<UpdateArmyCountSystem>().AsSingle();
            Container.Bind<ArmyCountEventSystem>().AsSingle();
            Container.Bind<GameSystems>().AsSingle();
            Container.Bind<GameStateSystems>().AsSingle();
            Container.Bind<MultiDestroySystem>().AsSingle();
            Container.Bind<RootSystems>().AsSingle();
        }
    }
}