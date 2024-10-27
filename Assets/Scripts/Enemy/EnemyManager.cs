using System;
using System.Collections.Generic;

namespace ShootEmUp
{
    public class EnemyManager : IGameStartListener, IGameUpdateListener, IDisposable, INonLazy
    {
        private readonly IGameManager _gameManager;
        private readonly ICooldownEnemySpawner _enemySpawner;
        private readonly IEnemyActivator _enemyActivator;
        private readonly List<Enemy> _activeEnemies = new();

        public EnemyManager(
            IGameManager gameManager,
            ICooldownEnemySpawner enemySpawner,
            IEnemyActivator enemyActivator,
            int maxActiveEnemiesCount)
        {
            _gameManager = gameManager;
            _enemySpawner = enemySpawner;
            _enemyActivator = enemyActivator;

            _gameManager.RegisterListener(this);
            _enemySpawner.AddSpawnCondition(() => _activeEnemies.Count < maxActiveEnemiesCount);
            _enemySpawner.OnEnemySpawned += OnEnemySpawned;
        }

        public void OnGameStart() => _enemySpawner.StartSpawnEnemies();

        public void OnGameUpdate(float deltaTime)
        {
            for (var index = 0; index < _activeEnemies.Count; index++)
            {
                _activeEnemies[index].OnGameUpdate(deltaTime);;
            }
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            _enemyActivator.Activate(enemy);
            _activeEnemies.Add(enemy);
            enemy.OnDeath += OnEnemyDeath;
        }

        private void OnEnemyDeath(Enemy enemy)
        {
            enemy.OnDeath -= OnEnemyDeath;
            _activeEnemies.Remove(enemy);
        }

        public void Dispose()
        {
            _gameManager.UnregisterListener(this);
            _enemySpawner.OnEnemySpawned -= OnEnemySpawned;
        }
    }
}