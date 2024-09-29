using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private Transform _worldContainer;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private PositionsGroup _spawnPositions;
        [SerializeField] private int _maxActiveEnemiesCount = 7;
        [SerializeField] private int _spawnEnemiesCooldown = 1;

        [Header("Attack")]
        [SerializeField] private GameObject _attackTarget;
        [SerializeField] private PositionsGroup _attackPositions;

        private readonly List<GameObject> _activeEnemies = new();
        private GameObjectPool _pool;

        private bool CanSpawnEnemy => _activeEnemies.Count < _maxActiveEnemiesCount;

        [UsedImplicitly]
        private void Awake()
        {
            _pool = new GameObjectPool(_enemyPrefab.gameObject, _poolContainer, _maxActiveEnemiesCount);
        }

        [UsedImplicitly]
        private void Start() => StartCoroutine(SpawnEnemiesByCooldown());


        private IEnumerator SpawnEnemiesByCooldown()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnEnemiesCooldown);

                if (CanSpawnEnemy)
                {
                    GameObject enemyObject = SpawnEnemyObject();
                    ActivateEnemyAI(enemyObject);
                }
            }
        }

        private GameObject SpawnEnemyObject()
        {
            GameObject enemyObject = _pool.Get();
            enemyObject.transform.SetParent(_worldContainer);
            enemyObject.transform.position = _spawnPositions.GetRandom();
            enemyObject.SetActive(true);
            _activeEnemies.Add(enemyObject);
            return enemyObject;
        }

        private void ActivateEnemyAI(GameObject enemyObject)
        {
            Vector2 attackPosition = _attackPositions.GetRandom();
            EnemyAI enemyAI = enemyObject.GetComponent<EnemyAI>();
            enemyAI.OnDeath += OnEnemyDeath;
            enemyAI.Initialize(attackPosition, _attackTarget);
            enemyAI.Activate();
        }

        private void OnEnemyDeath(EnemyAI enemyAI)
        {
            enemyAI.OnDeath -= OnEnemyDeath;
            GameObject enemyObject = enemyAI.gameObject;
            _activeEnemies.Remove(enemyObject);
            _pool.Release(enemyObject);
        }
    }
}