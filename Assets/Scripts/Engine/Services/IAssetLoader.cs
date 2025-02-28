using UnityEngine;

namespace Engine.Services
{
    public interface IAssetLoader
    {
        GameObject LoadAsset(string name);
    }
}