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
        public event Action OnStartResumeButtonClicked;
        public event Action OnPauseButtonClicked;

        [SerializeField] private TMP_Text _countdownText;

        [SerializeField] private Button _startResumeButton;
        [SerializeField] private TMP_Text _startResumeButtonText;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private TMP_Text _pauseButtonText;

        [UsedImplicitly]
        private void Awake()
        {
            HideCountdown();
            HideStartResumeButton();
            HidePauseButton();

            _startResumeButton.onClick.AddListener(OnStartResumeClick);
            _pauseButton.onClick.AddListener(OnPauseClick);
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            _startResumeButton.onClick.RemoveListener(OnStartResumeClick);
            _pauseButton.onClick.RemoveListener(OnPauseClick);
        }

        public void ShowCountdown() => _countdownText.gameObject.SetActive(true);

        public void HideCountdown() => _countdownText.gameObject.SetActive(false);

        public void SetCountdownValue(string text) => _countdownText.SetText(text);

        public void ShowStartResumeButton(string buttonText)
        {
            _startResumeButtonText.text = buttonText;
            _startResumeButton.gameObject.SetActive(true);
        }

        public void HideStartResumeButton() => _startResumeButton.gameObject.SetActive(false);

        public void ShowPauseButton(string buttonText)
        {
            _pauseButtonText.text = buttonText;
            _pauseButton.gameObject.SetActive(true);
        }

        public void HidePauseButton() => _pauseButton.gameObject.SetActive(false);

        private void OnStartResumeClick() => OnStartResumeButtonClicked.SafeInvoke();

        private void OnPauseClick() => OnPauseButtonClicked.SafeInvoke();
    }
}