using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private PauseScreen _pauseScreen;

        [UsedImplicitly]
        private void OnEnable() => _button.onClick.AddListener(OnClick);

        [UsedImplicitly]
        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        public void SetPauseScreen(PauseScreen pauseScreen) => _pauseScreen = pauseScreen;

        private void OnClick() => _pauseScreen.Show();
    }
}