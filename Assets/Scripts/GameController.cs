using System.Collections.Generic;
using Configs;
using UI;

public class GameController
{
    private readonly Entitas.Systems _systems;

    public GameController(
        Contexts contexts,
        UnitConfig unitConfig,
        ProjectileConfig projectileConfig,
        SpawnArmyConfig spawnArmyConfig,
        List<UiArmyCountView> uiArmyCountViews)
    {
        _systems = new RootSystems(contexts, unitConfig, projectileConfig, spawnArmyConfig, uiArmyCountViews);
    }

    public void Initialize() => _systems.Initialize();

    public void Execute()
    {
        _systems.Execute();
        _systems.Cleanup();
    }
}