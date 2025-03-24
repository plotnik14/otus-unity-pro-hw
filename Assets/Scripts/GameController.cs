public class GameController
{
    private readonly Entitas.Systems _systems;

    public GameController(RootSystems rootSystems) => _systems = rootSystems;

    public void Initialize() => _systems.Initialize();

    public void Execute()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    public void TearDown() => _systems.TearDown();
}