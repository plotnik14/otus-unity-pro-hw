using Core.Systems;
using DefaultNamespace;
using Infrastructure;
using View;

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
        Add<MultiDestroySystem>();
    }
}