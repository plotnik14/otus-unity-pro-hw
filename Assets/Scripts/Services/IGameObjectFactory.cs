using UnityEngine;

namespace Services
{
    public interface IGameObjectFactory
    {
        GameObject Instantiate(GameObject prefab, Transform parent);
    }
}