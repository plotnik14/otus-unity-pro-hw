using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace SampleGame
{
    public sealed class AssetsLoader : IDisposable
    {
        private readonly Dictionary<string, AsyncOperationHandle> _loadedAssets = new();

        public async UniTask<T> LoadAsset<T>(string assetName)
        {
            if (_loadedAssets.TryGetValue(assetName, out AsyncOperationHandle savedHandle))
            {
                await savedHandle;
                return (T) savedHandle.Result;
            }

            AsyncOperationHandle<T> operationHandle = Addressables.LoadAssetAsync<T>(assetName);
            _loadedAssets.Add(assetName, operationHandle);
            return await operationHandle;
        }

        public async UniTask<T> LoadAsset<T>(AssetReference assetReference)
        {
            return await LoadAsset<T>(assetReference.RuntimeKey.ToString());
        }

        public async UniTask<T> LoadAsset<T>(IResourceLocation resourceLocation)
        {
            return await LoadAsset<T>(resourceLocation.PrimaryKey);
        }

        public void UnloadAsset(string assetName)
        {
            if (_loadedAssets.TryGetValue(assetName, out AsyncOperationHandle savedHandle))
            {
                Addressables.Release(savedHandle);
                _loadedAssets.Remove(assetName);
            }
        }

        public void UnloadAsset(AssetReference assetReference)
        {
            UnloadAsset(assetReference.RuntimeKey.ToString());
        }

        public void UnloadAsset(IResourceLocation resourceLocation)
        {
            UnloadAsset(resourceLocation.PrimaryKey);
        }

        public void Dispose()
        {
            foreach (AsyncOperationHandle handle in _loadedAssets.Values)
            {
                Addressables.Release(handle);
            }

            _loadedAssets.Clear();
        }
    }
}