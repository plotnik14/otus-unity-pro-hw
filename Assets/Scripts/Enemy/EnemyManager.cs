using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyManager : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameUpdateListener
    {
        [SerializeField] private GameManager _gameManager;

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

        private readonly List<EnemyAI> _activeEnemies = new();
        private GameObjectPool _pool;
        private Coroutine _spawnEnemiesByCooldownCoroutine;

        private bool CanSpawnEnemy => _activeEnemies.Count < _maxActiveEnemiesCount;

        [UsedImplicitly]
        private void Awake()
        {
            _pool = new GameObjectPool(_enemyPrefab.gameObject, _poolContainer, _maxActiveEnemiesCount);
            _gameManager.RegisterListener(this);
        }

        public void OnGameStart() => _spawnEnemiesByCooldownCoroutine = StartCoroutine(SpawnEnemiesByCooldown());

        public void OnGamePause()
        {
            StopCoroutine(_spawnEnemiesByCooldownCoroutine);

            for (var index = 0; index < _activeEnemies.Count; index++)
            {
                _activeEnemies[index].OnGamePause();
            }
        }

        public void OnGameResume()
        {
            _spawnEnemiesByCooldownCoroutine = StartCoroutine(SpawnEnemiesByCooldown());

            for (var index = 0; index < _activeEnemies.Count; index++)
            {
                _activeEnemies[index].OnGameResume();
            }
        }

        public void OnGameUpdate(float deltaTime)
        {
            for (var index = 0; index < _activeEnemies.Count; index++)
            {
                _activeEnemies[index].OnGameUpdate(deltaTime);;
            }
        }

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
            return enemyObject;
        }

        private void ActivateEnemyAI(GameObject enemyObject)
        {
            Vector2 attackPosition = _attackPositions.GetRandom();
            EnemyAI enemyAI = enemyObject.GetComponent<EnemyAI>();
            enemyAI.OnDeath += OnEnemyDeath;
            enemyAI.Initialize(attackPosition, _attackTarget);
            enemyAI.Activate();
            _activeEnemies.Add(enemyAI);
        }

        private void OnEnemyDeath(EnemyAI enemyAI)
        {
            enemyAI.OnDeath -= OnEnemyDeath;
            _activeEnemies.Remove(enemyAI);
            _pool.Release(enemyAI.gameObject);
        }
    }
}