using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace Trees
{
    public class TreeGroup : MonoBehaviour
    {
        public event Action<int> OnTreesCountChanged;

        [SerializeField] private Tree _treePrefab;
        [SerializeField] private List<Tree> _trees;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private float _cooldown;

        private float _lastSpawnTime;
        private int _spawnPointIndex = 0;

        private Vector3 CurrentSpawnPoint => _spawnPoints[_spawnPointIndex].position;

        public int CurrentTreesCount => _trees.Count;

        private bool CanSpawnTree
        {
            get
            {
                if (_lastSpawnTime + _cooldown > Time.time)
                    return false;

                if (_trees.Count >= _spawnPoints.Count)
                    return false;

                return true;
            }
        }

        [UsedImplicitly]
        private void Update()
        {
            if (CanSpawnTree)
            {
                SpawnTree();
                _lastSpawnTime = Time.time;
            }
        }

        [CanBeNull]
        public Tree GetNearestTree(Vector3 currentPosition)
        {
            Tree nearestTree = null;
            float minSqrDistance = float.MaxValue;

            foreach (Tree tree in _trees)
            {
                Vector3 treePosition = tree.transform.position;
                float sqrDistance = (treePosition - currentPosition).sqrMagnitude;

                if (sqrDistance < minSqrDistance)
                {
                    nearestTree = tree;
                    minSqrDistance = sqrDistance;
                }
            }

            return nearestTree;
        }

        private void SpawnTree()
        {
            Tree spawnedTree = Instantiate(_treePrefab, CurrentSpawnPoint, Quaternion.identity, transform);
            spawnedTree.OnChopped += OnTreeChopped;
            _trees.Add(spawnedTree);
            SetNextSpawnPoint();
            OnTreesCountChanged.SafeInvoke(_trees.Count);
        }

        private void SetNextSpawnPoint() => _spawnPointIndex = ++_spawnPointIndex % _spawnPoints.Count;

        [Button("Spawn Tree")]
        private void SpawnTreeDebug()
        {
            if (_trees.Count >= _spawnPoints.Count)
                return;

            SpawnTree();
        }

        private void OnTreeChopped(Tree choppedTree)
        {
            choppedTree.OnChopped -= OnTreeChopped;
            _trees.Remove(choppedTree);
            OnTreesCountChanged.SafeInvoke(_trees.Count);
            Destroy(choppedTree.gameObject);
        }
    }
}