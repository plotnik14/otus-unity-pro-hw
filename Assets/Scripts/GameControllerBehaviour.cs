using System.Collections.Generic;
using Configs;
using JetBrains.Annotations;
using UI;
using UnityEngine;

public class GameControllerBehaviour : MonoBehaviour
{
    [SerializeField] private UnitConfig _unitConfig;
    [SerializeField] private ProjectileConfig _projectileConfig;
    [SerializeField] private SpawnArmyConfig _spawnArmyConfig;

    [SerializeField] private UiArmyCountView _redUiArmyCountView;
    [SerializeField] private UiArmyCountView _blueUiArmyCountView;

    private GameController _gameController;

    [UsedImplicitly]
    private void Awake()
    {
        _gameController = new GameController(
            Contexts.sharedInstance,
            _unitConfig,
            _projectileConfig,
            _spawnArmyConfig,
            new List<UiArmyCountView> { _redUiArmyCountView, _blueUiArmyCountView }); // ToDo временное решение. Переделать
    }

    [UsedImplicitly]
    private void Start() => _gameController.Initialize();

    [UsedImplicitly]
    private void Update() => _gameController.Execute();
}