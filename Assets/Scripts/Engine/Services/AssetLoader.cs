using UnityEngine;

namespace Engine.Services
{
    public class AssetLoader : IAssetLoader
    {
        public GameObject LoadAsset(string name)
        {
            return Resources.Load<GameObject>(name);
        }
    }
}