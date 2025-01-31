using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public class LoadLocationTrigger : MonoBehaviour
    {
        [SerializeField] private AssetReference _locationAsset;

        private LocationLoader _locationLoader;
        private bool _isLocationLoaded;

        [Inject]
        private void Construct(LocationLoader locationLoader) => _locationLoader = locationLoader;

        [UsedImplicitly]
        private void OnTriggerEnter(Collider _)
        {
            if (_isLocationLoaded)
                return;

            LoadLocationAsync().Forget();
        }

        private async UniTaskVoid LoadLocationAsync()
        {
            await _locationLoader.LoadLocationAsync(_locationAsset);
            _isLocationLoaded = true;
        }
    }
}