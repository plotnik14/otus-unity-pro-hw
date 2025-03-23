using Configs;
using Core.Systems;
using Services;

namespace Core
{
    public class GameSystems : Feature
    {
        public GameSystems(
            Contexts contexts,
            UnitConfig unitConfig,
            ProjectileConfig projectileConfig,
            SpawnArmyConfig spawnArmyConfig)
        {
            // ToDo DI
            ITimeService timeService = new UnityTimeService();
            IAssetLoader assetLoader = new AssetLoader();
            IGameObjectFactory objectFactory = new GameObjectFactory();

            // Initialize
            Add(new SpawnArmySystem(contexts.game, unitConfig, spawnArmyConfig));

            // Execute
            Add(new MovementSystem(contexts.game, timeService));
            Add(new RotationSystem(contexts.game));
            Add(new UnitCollisionSystem(contexts.game, projectileConfig));
            Add(new ProjectileCollisionSystem(contexts.game));
            Add(new DealDamageSystem(contexts.game));
            Add(new ReleaseTargetSystem(contexts.game));
            Add(new UnitDieSystem(contexts.game));
            Add(new FindTargetSystem(contexts.game, unitConfig));
            Add(new AttackCooldownSystem(contexts.game, timeService));
            Add(new AttackSystem(contexts.game));
            Add(new LookAtTargetSystem(contexts.game));
            Add(new SpawnProjectileSystem(contexts.game, projectileConfig));
            Add(new AddAttackCooldownSystem(contexts.game, unitConfig));
            Add(new LifeTimeSystem(contexts.game, timeService));

            // View
            Add(new MultiCreateViewSystem(contexts, assetLoader, objectFactory));

            // Events (Generated)
            Add(new GameDestroyedEventSystem(contexts));
            Add(new PositionEventSystem(contexts));
            Add(new RotationEventSystem(contexts));
            Add(new TeamEventSystem(contexts));

            // Cleanup
            Add(new AttackRequestCleanup(contexts.game));
            Add(new DieRequestCleanup(contexts.game));
        }
    }
}