using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ShootEmUp
{
    public class GameObjectPool
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;
        private readonly Stack<GameObject> _pool;

        public GameObjectPool(GameObject prefab, Transform container, int warmUpCount = 0)
        {
            _prefab = prefab;
            _container = container;
            _pool = new Stack<GameObject>();
            WarmUp(warmUpCount);
        }

        public GameObject Get()
        {
            GameObject gameObject = _pool.Pop();
            gameObject ??= CreateInstance();
            gameObject.SetActive(true);
            return gameObject;
        }

        public void Release(GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(_container);
            _pool.Push(gameObject);
        }

        private GameObject CreateInstance() => Object.Instantiate(_prefab, _container);

        private void WarmUp(int warmUpCount)
        {
            for (int i = 0; i < warmUpCount; i++)
            {
                _pool.Push(CreateInstance());
            }
        }
    }
}