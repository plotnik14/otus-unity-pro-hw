using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using Zenject;

namespace SampleGame
{
    public sealed class ApplicationStarter : IInitializable
    {
        private readonly AssetsLoader _assetsLoader;

        public ApplicationStarter(AssetsLoader assetsLoader) => _assetsLoader = assetsLoader;

        void IInitializable.Initialize() => PreloadAssets().Forget();

        private async UniTaskVoid PreloadAssets()
        {
            var operationHandle = Addressables.LoadResourceLocationsAsync(AddressablesLabels.PRELOAD_LABEL);

            await operationHandle;

            foreach (IResourceLocation resourceLocation in operationHandle.Result)
            {
                _assetsLoader.LoadAsset<GameObject>(resourceLocation).Forget();
            }
        }
    }
}