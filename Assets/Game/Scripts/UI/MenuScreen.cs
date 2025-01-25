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
        private GameLoader _gameLoader;
        
        [Inject]
        public void Construct(ApplicationExiter applicationFinisher, GameLoader gameLoader)
        {
            _gameLoader = gameLoader;
            _applicationExiter = applicationFinisher;
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            _startButton.onClick.AddListener(_gameLoader.LoadGame);
            _exitButton.onClick.AddListener(ApplicationExiter.ExitApp);
        }

        [UsedImplicitly]
        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(_gameLoader.LoadGame);
            _exitButton.onClick.RemoveListener(ApplicationExiter.ExitApp);
        }
    }
}