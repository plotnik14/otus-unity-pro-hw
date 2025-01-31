using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SampleGame
{
    public sealed class PauseScreen : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;

        private SceneLoader _sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            gameObject.SetActive(false);
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(Hide);
            _exitButton.onClick.AddListener(Exit);
        }

        [UsedImplicitly]
        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(Hide);
            _exitButton.onClick.RemoveListener(Exit);
        }

        public void Show()
        {
            Time.timeScale = 0; //KISS
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            Time.timeScale = 1; //KISS
            gameObject.SetActive(false);
        }

        private void Exit() => _sceneLoader.LoadSceneAsync(SceneNames.MENU);
    }
}