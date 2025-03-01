using Core.Systems;
using Engine.Configs;
using Engine.Services;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private UnitConfig _unitConfig;
        [SerializeField] private ProjectileConfig _projectileConfig;
        [SerializeField] private SpawnArmyConfig _spawnArmyConfig;

        private Entitas.Systems _systems;

        [UsedImplicitly]
        private void Start()
        {
            ITimeService timeService = new UnityTimeService();
            IAssetLoader assetLoader = new AssetLoader();
            IGameObjectFactory objectFactory = new GameObjectFactory();
            Contexts contexts = Contexts.sharedInstance;

            _systems = new Feature("Systems")
                // Initialize
                .Add(new SpawnArmySystem(contexts.game, _unitConfig, _spawnArmyConfig))

                // Execute
                .Add(new MovementSystem(contexts.game, timeService))
                .Add(new RotationSystem(contexts.game))
                .Add(new UnitCollisionSystem(contexts.game, _projectileConfig))
                .Add(new ProjectileCollisionSystem(contexts.game))
                .Add(new DealDamageSystem(contexts.game))
                .Add(new ReleaseTargetSystem(contexts.game))
                .Add(new FindTargetSystem(contexts.game, _unitConfig))
                .Add(new AttackCooldownSystem(contexts.game, timeService))
                .Add(new AttackSystem(contexts.game))
                .Add(new LookAtTargetSystem(contexts.game))
                .Add(new SpawnProjectileSystem(contexts.game, _projectileConfig))
                .Add(new AddAttackCooldownSystem(contexts.game, _unitConfig))

                // View
                .Add(new CreateViewSystem(contexts.game, assetLoader, objectFactory))

                // Events (Generated)
                .Add(new DestroyedEventSystem(contexts))
                .Add(new PositionEventSystem(contexts))
                .Add(new RotationEventSystem(contexts))
                .Add(new TeamEventSystem(contexts))

                // Cleanup
                .Add(new AttackRequestCleanup(contexts.game))
                .Add(new DestroyedCleanupSystem(contexts.game));

            _systems.Initialize();
        }

        [UsedImplicitly]
        private void Update()
        {
            _systems.Execute();
            _systems.Cleanup();
        }
    }
}