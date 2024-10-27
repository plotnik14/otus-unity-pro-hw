using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public class ObjectPool<T> : IObjectPool<T> where T : Component, IReusable<T>
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Stack<T> _pool;
        private readonly IObjectResolver _diContainer;

        public ObjectPool(
            T prefab,
            Transform parent,
            IObjectResolver diContainer,
            int warmUpCount = 0)
        {
            _prefab = prefab;
            _parent = parent;
            _diContainer = diContainer;

            _pool = new Stack<T>();
            WarmUp(warmUpCount);
        }

        public T Get()
        {
            if (!_pool.TryPop(out T instance))
            {
                instance = CreateInstance();
            }

            instance.OnInstanceReleased += OnInstanceReleased;
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Dispose() => _pool.Clear();

        private void OnInstanceReleased(T instance)
        {
            instance.OnInstanceReleased -= OnInstanceReleased;
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(_parent);
            _pool.Push(instance);
        }

        private T CreateInstance()
        {
            T instance = _diContainer.Instantiate(_prefab, _parent);
            instance.gameObject.SetActive(false);
            return instance;
        }

        private void WarmUp(int warmUpCount)
        {
            for (int i = 0; i < warmUpCount; i++)
            {
                _pool.Push(CreateInstance());
            }
        }
    }
}