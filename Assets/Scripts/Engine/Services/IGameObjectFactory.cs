using UnityEngine;

namespace Engine.Services
{
    public interface IGameObjectFactory
    {
        GameObject Instantiate(GameObject prefab, Transform parent);
    }
}