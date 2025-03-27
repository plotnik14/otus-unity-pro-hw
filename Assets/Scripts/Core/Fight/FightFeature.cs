using Core.Fight.Systems;
using Infrastructure;

namespace Core.Fight
{
    public class FightFeature : ExtendedFeature
    {
        public FightFeature(SystemProvider provider) : base(provider)
        {
            Add<DealDamageRxSystem>();
            Add<ReleaseTargetRxSystem>();
            Add<UnitDieRxSystem>();
            Add<FindTargetExSystem>();
            Add<AttackCooldownExSystem>();
            Add<AttackRxSystem>();
            Add<LookAtTargetRxSystem>();
            Add<SpawnProjectileRxSystem>();
            Add<AddAttackCooldownRxSystem>();
            Add<LifeTimeExSystem>();
            Add<AttackRequestCleanupSystem>();
            Add<DieRequestCleanupSystem>();
        }
    }
}