using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public class MenuSceneUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private AssetReference _menuScreenAsset;

        private AssetsLoader _assetsLoader;
        private ObjectsFactory _objectsFactory;

        [Inject]
        private void Construct(AssetsLoader assetsLoader, ObjectsFactory objectsFactory)
        {
            _assetsLoader = assetsLoader;
            _objectsFactory = objectsFactory;
        }

        [UsedImplicitly]
        private void Start()
        {
            UniTask<GameObject> loadScreenTask = _assetsLoader.LoadAsset<GameObject>(_menuScreenAsset);
            loadScreenTask.GetAwaiter().OnCompleted(() =>
            {
                GameObject _menuScreenPrefab = loadScreenTask.GetAwaiter().GetResult();
                _objectsFactory.Instantiate(_menuScreenPrefab, _canvas.transform);
            });
        }
    }
}