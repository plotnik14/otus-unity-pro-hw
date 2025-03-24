using Core.Systems;
using Infrastructure;

namespace Core
{
    public class GameSystems : ExtendedFeature
    {
        public GameSystems(SystemProvider provider) : base(provider)
        {
            // Initialize
            Add<SpawnArmySystem>();

            // Execute
            Add<MovementSystem>();
            Add<RotationSystem>();
            Add<UnitCollisionSystem>();
            Add<ProjectileCollisionSystem>();
            Add<DealDamageSystem>();
            Add<ReleaseTargetSystem>();
            Add<UnitDieSystem>();
            Add<FindTargetSystem>();
            Add<AttackCooldownSystem>();
            Add<AttackSystem>();
            Add<LookAtTargetSystem>();
            Add<SpawnProjectileSystem>();
            Add<AddAttackCooldownSystem>();
            Add<LifeTimeSystem>();

            // View
            Add<MultiCreateViewSystem>();

            // Events (Generated)
            Add<GameDestroyedEventSystem>();
            Add<PositionEventSystem>();
            Add<RotationEventSystem>();
            Add<TeamEventSystem>();

            // Cleanup
            Add<AttackRequestCleanup>();
            Add<DieRequestCleanup>();
        }
    }
}