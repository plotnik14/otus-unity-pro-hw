using System;

namespace ShootEmUp
{
    public interface ICooldownEnemySpawner
    {
        event Action<Enemy> OnEnemySpawned;

        void AddSpawnCondition(Func<bool> spawnCondition);
        void StartSpawnEnemies();
        void StopSpawnEnemies();
    }
}