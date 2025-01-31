using UnityEngine;
using Zenject;

namespace SampleGame
{
    public class ObjectsFactory
    {
        private readonly DiContainer _diContainer;

        public ObjectsFactory(DiContainer diContainer) => _diContainer = diContainer;

        public GameObject Instantiate(Object prefab, Transform parentTransform)
        {
            return _diContainer.InstantiatePrefab(prefab, parentTransform);
        }
    }
}