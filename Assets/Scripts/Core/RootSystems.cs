using Core.ArmyCount;
using Core.BattleInitialization;
using Core.Collisions;
using Core.Destroy;
using Core.Fight;
using Core.Movement;
using Core.Views;
using Infrastructure;

namespace Core
{
    public class RootSystems : ExtendedFeature
    {
        public RootSystems(SystemProvider provider) : base(provider)
        {
            Add<BattleInitializationFeature>();
            Add<MovementFeature>();
            Add<CollisionProcessingFeature>();
            Add<FightFeature>();
            Add<ArmyCountFeature>();
            Add<ViewProcessingFeature>();
            Add<DestroyFeature>();
        }
    }
}