using UnityEngine;

namespace Services
{
    public class AssetLoader : IAssetLoader
    {
        public GameObject LoadAsset(string name)
        {
            return Resources.Load<GameObject>(name);
        }
    }
}