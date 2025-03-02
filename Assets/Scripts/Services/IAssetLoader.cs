using UnityEngine;

namespace Services
{
    public interface IAssetLoader
    {
        GameObject LoadAsset(string name);
    }
}