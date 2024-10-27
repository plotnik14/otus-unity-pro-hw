using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class CooldownEnemySpawner : ICooldownEnemySpawner, IDisposable, INonLazy
    {
        public event Action<Enemy> OnEnemySpawned;

        private readonly Transform _worldContainer;
        private readonly IObjectPool<Enemy> _pool;
        private readonly PositionsGroup _spawnPositions;
        private readonly int _spawnCooldown;
        private readonly CompositeCondition _canSpawnCondition = new();
        private CancellationTokenSource _spawnCts;

        public CooldownEnemySpawner(
            Transform worldContainer,
            IObjectPool<Enemy> pool,
            int spawnCooldown,
            PositionsGroup spawnPositions)
        {
            _worldContainer = worldContainer;
            _pool = pool;
            _spawnCooldown = spawnCooldown;
            _spawnPositions = spawnPositions;
        }

        public void Dispose()
        {
            _spawnCts.CancelAndDispose();
            _spawnCts = null;
            _canSpawnCondition.Dispose();
        }

        public void AddSpawnCondition(Func<bool> spawnCondition) => _canSpawnCondition.AddCondition(spawnCondition);

        public void StartSpawnEnemies()
        {
            _spawnCts.CancelAndDispose();
            _spawnCts = new CancellationTokenSource();
            SpawnEnemiesByCooldownAsync(_spawnCts.Token).Forget();
        }

        public void StopSpawnEnemies()
        {
            _spawnCts.CancelAndDispose();
            _spawnCts = null;
        }

        private async UniTaskVoid SpawnEnemiesByCooldownAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_spawnCooldown, cancellationToken: cancellationToken);

                if (_canSpawnCondition.IsTrue)
                {
                    SpawnEnemyObject();
                }
            }
        }

        private void SpawnEnemyObject()
        {
            Enemy enemy = _pool.Get();
            enemy.transform.SetParent(_worldContainer);
            enemy.transform.position = _spawnPositions.GetRandom();
            enemy.gameObject.SetActive(true);
            OnEnemySpawned.SafeInvoke(enemy);
        }
    }
}