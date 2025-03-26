using Core;
using Core.Systems;
using GameState;
using Infrastructure;
using UI;

public class RootSystems : ExtendedFeature
{
    public RootSystems(SystemProvider provider) : base(provider)
    {
        Add<GameSystems>();
        Add<GameStateSystems>();
        Add<InitArmyStatisticsUiSystem>(); // ToDo move to UI feature?
        Add<MultiDestroySystem>();
    }
}