using Core;
using Core.Systems;
using GameState;
using Infrastructure;

public class RootSystems : ExtendedFeature
{
    public RootSystems(SystemProvider provider) : base(provider)
    {
        Add<GameSystems>();
        Add<GameStateSystems>();
        Add<MultiDestroySystem>();
    }
}