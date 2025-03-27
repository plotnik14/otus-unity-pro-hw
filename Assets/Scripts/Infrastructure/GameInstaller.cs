using System.Collections.Generic;
using Core;
using Core.ArmyCount;
using Core.ArmyCount.Systems;
using Core.BattleInitialization;
using Core.Collisions;
using Core.Collisions.Systems;
using Core.Configs;
using Core.Destroy;
using Core.Fight;
using Core.Fight.Systems;
using Core.Movement;
using Core.Movement.Systems;
using Core.Views;
using Core.Views.Systems;
using Services;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UnitConfig _unitConfig;
        [SerializeField] private ProjectileConfig _projectileConfig;
        [SerializeField] private SpawnArmyConfig _spawnArmyConfig;

        [SerializeField] private Transform _gameEntitiesParent;
        [SerializeField] private Transform _uiEntitiesParent;

        private Dictionary<string, Transform> _parentByContextName;

        public override void InstallBindings()
        {
            BindConfiguration();
            BindServices();
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

        private void BindContexts()
        {
            Contexts contexts = Contexts.sharedInstance;
            Container.BindInstance(contexts).AsSingle();
            Container.BindInterfacesAndSelfTo<GameContext>().FromInstance(contexts.game).AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateContext>().FromInstance(contexts.gameState).AsSingle();
            Container.BindInterfacesAndSelfTo<InputContext>().FromInstance(contexts.input).AsSingle();
            Container.BindInterfacesAndSelfTo<UiContext>().FromInstance(contexts.ui).AsSingle();

            _parentByContextName = new Dictionary<string, Transform>
            {
                { contexts.game.contextInfo.name, _gameEntitiesParent },
                { contexts.ui.contextInfo.name, _uiEntitiesParent },
            };
        }

        private void BindSystems()
        {
            Container.Bind<SpawnArmyInitSystem>().AsSingle();
            Container.Bind<MovementExSystem>().AsSingle();
            Container.Bind<RotationExSystem>().AsSingle();
            Container.Bind<UnitCollisionRxSystem>().AsSingle();
            Container.Bind<ProjectileCollisionRxSystem>().AsSingle();
            Container.Bind<DealDamageRxSystem>().AsSingle();
            Container.Bind<ReleaseTargetRxSystem>().AsSingle();
            Container.Bind<UnitDieRxSystem>().AsSingle();
            Container.Bind<FindTargetExSystem>().AsSingle();
            Container.Bind<AttackCooldownExSystem>().AsSingle();
            Container.Bind<AttackRxSystem>().AsSingle();
            Container.Bind<LookAtTargetRxSystem>().AsSingle();
            Container.Bind<SpawnProjectileRxSystem>().AsSingle();
            Container.Bind<AddAttackCooldownRxSystem>().AsSingle();
            Container.Bind<LifeTimeExSystem>().AsSingle();
            Container.Bind<CreateViewMultiRxSystem>().AsSingle().WithArguments(_parentByContextName);
            Container.Bind<GameDestroyedEventSystem>().AsSingle();
            Container.Bind<GameStateDestroyedEventSystem>().AsSingle();
            Container.Bind<UiDestroyedEventSystem>().AsSingle();
            Container.Bind<PositionEventSystem>().AsSingle();
            Container.Bind<RotationEventSystem>().AsSingle();
            Container.Bind<TeamEventSystem>().AsSingle();
            Container.Bind<AttackRequestCleanupSystem>().AsSingle();
            Container.Bind<DieRequestCleanupSystem>().AsSingle();
            Container.Bind<ArmyCountersInitSystem>().AsSingle();
            Container.Bind<ArmyStatisticsUiInitSystem>().AsSingle();
            Container.Bind<UpdateArmyCountRxSystem>().AsSingle();
            Container.Bind<ArmyCountEventSystem>().AsSingle();
            Container.Bind<DestroyMultiRxSystem>().AsSingle();
            Container.Bind<BattleInitializationFeature>().AsSingle();
            Container.Bind<MovementFeature>().AsSingle();
            Container.Bind<CollisionProcessingFeature>().AsSingle();
            Container.Bind<FightFeature>().AsSingle();
            Container.Bind<ArmyCountFeature>().AsSingle();
            Container.Bind<ViewProcessingFeature>().AsSingle();
            Container.Bind<DestroyFeature>().AsSingle();
            Container.Bind<RootSystems>().AsSingle();
        }
    }
}