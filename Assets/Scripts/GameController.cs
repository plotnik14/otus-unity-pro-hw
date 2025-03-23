using Configs;

public class GameController
{
    private readonly Entitas.Systems _systems;

    public GameController(
        Contexts contexts,
        UnitConfig unitConfig,
        ProjectileConfig projectileConfig,
        SpawnArmyConfig spawnArmyConfig)
    {
        _systems = new RootSystems(contexts, unitConfig, projectileConfig, spawnArmyConfig);
    }

    public void Initialize() => _systems.Initialize();

    public void Execute()
    {
        _systems.Execute();
        _systems.Cleanup();
    }
}