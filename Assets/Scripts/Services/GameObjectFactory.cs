using UnityEngine;
using Zenject;

namespace Services
{
    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly DiContainer _diContainer;

        public GameObjectFactory(DiContainer diContainer) => _diContainer = diContainer;

        public GameObject Instantiate(GameObject prefab, Transform parent)
        {
            return _diContainer.InstantiatePrefab(prefab, parent);
        }
    }
}