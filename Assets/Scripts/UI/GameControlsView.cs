using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class GameControlsView : MonoBehaviour
    {
        public event Action OnPlayButtonClicked;
        public event Action OnPauseButtonClicked;

        [SerializeField] private TMP_Text _countdownText;

        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_Text _playButtonText;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private TMP_Text _pauseButtonText;

        [UsedImplicitly]
        private void Awake()
        {
            HideCountdown();
            HidePlayButton();
            HidePauseButton();

            _playButton.onClick.AddListener(OnPlayClick);
            _pauseButton.onClick.AddListener(OnPauseClick);
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayClick);
            _pauseButton.onClick.RemoveListener(OnPauseClick);
        }

        public void ShowCountdown() => _countdownText.gameObject.SetActive(true);

        public void HideCountdown() => _countdownText.gameObject.SetActive(false);

        public void SetCountdownValue(string text) => _countdownText.SetText(text);

        public void ShowPlayButton(string buttonText)
        {
            _playButtonText.text = buttonText;
            _playButton.gameObject.SetActive(true);
        }

        public void HidePlayButton() => _playButton.gameObject.SetActive(false);

        public void ShowPauseButton(string buttonText)
        {
            _pauseButtonText.text = buttonText;
            _pauseButton.gameObject.SetActive(true);
        }

        public void HidePauseButton() => _pauseButton.gameObject.SetActive(false);

        private void OnPlayClick() => OnPlayButtonClicked.SafeInvoke();

        private void OnPauseClick() => OnPauseButtonClicked.SafeInvoke();
    }
}