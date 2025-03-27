using Core.Systems;
using Infrastructure;

namespace DefaultNamespace
{
    public class FightFeature : ExtendedFeature
    {
        public FightFeature(SystemProvider provider) : base(provider)
        {
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
            Add<AttackRequestCleanup>();
            Add<DieRequestCleanup>();
        }
    }
}