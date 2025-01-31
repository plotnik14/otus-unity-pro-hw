using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SampleGame
{
    public class LocationLoader
    {
        private readonly AssetsLoader _assetsLoader;
        private readonly ObjectsFactory _objectsFactory;
        private readonly Transform _locationsContainer;
        private readonly HashSet<AssetReference> _loadedLocations = new();

        public LocationLoader(
            AssetsLoader assetsLoader,
            ObjectsFactory objectsFactory,
            Transform locationsContainer)
        {
            _assetsLoader = assetsLoader;
            _objectsFactory = objectsFactory;
            _locationsContainer = locationsContainer;
        }

        public async UniTask<GameObject> LoadLocationAsync(AssetReference locationAsset)
        {
            GameObject locationPrefab = await _assetsLoader.LoadAsset<GameObject>(locationAsset);
            _loadedLocations.Add(locationAsset);
            _objectsFactory.Instantiate(locationPrefab, _locationsContainer);
            return locationPrefab;
        }

        public void UnloadAllLocations()
        {
            foreach (AssetReference loadedLocation in _loadedLocations)
            {
                _assetsLoader.UnloadAsset(loadedLocation);
            }
        }
    }
}