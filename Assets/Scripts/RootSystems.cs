using Configs;
using Core;
using GameState;

public class RootSystems : Feature
{
    public RootSystems(
        Contexts contexts,
        UnitConfig unitConfig,
        ProjectileConfig projectileConfig,
        SpawnArmyConfig spawnArmyConfig)
    {
        Add(new GameSystems(contexts, unitConfig, projectileConfig, spawnArmyConfig));
        Add(new GameStateSystems(contexts));
    }
}