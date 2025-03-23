using Configs;
using JetBrains.Annotations;
using UnityEngine;

public class GameControllerBehaviour : MonoBehaviour
{
    [SerializeField] private UnitConfig _unitConfig;
    [SerializeField] private ProjectileConfig _projectileConfig;
    [SerializeField] private SpawnArmyConfig _spawnArmyConfig;

    private GameController _gameController;

    [UsedImplicitly]
    private void Awake()
    {
        _gameController = new GameController(Contexts.sharedInstance, _unitConfig, _projectileConfig, _spawnArmyConfig);
    }

    [UsedImplicitly]
    private void Start() => _gameController.Initialize();

    [UsedImplicitly]
    private void Update() => _gameController.Execute();
}