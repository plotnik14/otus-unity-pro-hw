using UnityEngine;

namespace Engine.Services
{
    public class GameObjectFactory : IGameObjectFactory
    {
        public GameObject Instantiate(GameObject prefab, Transform parent)
        {
            return Object.Instantiate(prefab, parent);
        }
    }
}