using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public class GameSceneUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private AssetReference _pauseScreenAsset;

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
            UniTask<GameObject> loadScreenTask = _assetsLoader.LoadAsset<GameObject>(_pauseScreenAsset);
            loadScreenTask.GetAwaiter().OnCompleted(() =>
            {
                GameObject _pauseScreenPrefab = loadScreenTask.GetAwaiter().GetResult();
                PauseScreen pauseScreen = _objectsFactory.Instantiate(_pauseScreenPrefab, _canvas.transform).GetComponent<PauseScreen>();
                pauseScreen.gameObject.SetActive(false);
                _pauseButton.SetPauseScreen(pauseScreen);
            });
        }
    }
}