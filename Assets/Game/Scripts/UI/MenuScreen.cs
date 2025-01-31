using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SampleGame
{
    public sealed class MenuScreen : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;
        
        private ApplicationExiter _applicationExiter;
        private SceneLoader _sceneLoader;
        
        [Inject]
        public void Construct(ApplicationExiter applicationExiter, SceneLoader sceneLoader)
        {
            _applicationExiter = applicationExiter;
            _sceneLoader = sceneLoader;
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(CloseGame);
        }

        [UsedImplicitly]
        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
            _exitButton.onClick.RemoveListener(CloseGame);
        }

        private void StartGame() => _sceneLoader.LoadSceneAsync(SceneNames.GAME);

        private void CloseGame() => _applicationExiter.ExitApp();
    }
}