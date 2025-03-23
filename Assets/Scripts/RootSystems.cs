using System.Collections.Generic;
using Configs;
using Core;
using Core.Systems;
using GameState;
using UI;

public class RootSystems : Feature
{
    public RootSystems(Contexts contexts,
        UnitConfig unitConfig,
        ProjectileConfig projectileConfig,
        SpawnArmyConfig spawnArmyConfig, List<UiArmyCountView> uiArmyCountViews)
    {
        Add(new GameSystems(contexts, unitConfig, projectileConfig, spawnArmyConfig));
        Add(new GameStateSystems(contexts, uiArmyCountViews));
        Add(new MultiDestroySystem(contexts));
    }
}