using System;
using System.Collections;
using JetBrains.Annotations;
using ShootEmUp;
using UnityEngine;
using Utils;

namespace UI
{
    public class GameControlsPresenter : MonoBehaviour
    {
        private const int COUNTDOWN_NUMBER = 3;
        private const int COUNTDOWN_DELAY_SEC = 1;

        private const string START_BUTTON_NAME = "Start";
        private const string RESUME_BUTTON_NAME = "Resume";
        private const string PAUSE_BUTTON_NAME = "Pause";

        [SerializeField] private GameControlsView _controlsView;
        [SerializeField] private GameManager _gameManager;

        [UsedImplicitly]
        private void Awake()
        {
            _controlsView.OnPlayButtonClicked += OnPlayButtonClicked;
            _controlsView.OnPauseButtonClicked += OnPauseButtonClicked;
        }

        [UsedImplicitly]
        private void Start() => _controlsView.ShowPlayButton(START_BUTTON_NAME);

        [UsedImplicitly]
        private void OnDestroy()
        {
            _controlsView.OnPlayButtonClicked -= OnPlayButtonClicked;
            _controlsView.OnPauseButtonClicked -= OnPauseButtonClicked;
        }

        private void OnPlayButtonClicked()
        {
            _controlsView.HidePlayButton();
            StartCoroutine(CountdownCoroutine(OnCountdownFinished));
        }

        private void OnPauseButtonClicked()
        {
            _gameManager.PauseGame();
            _controlsView.HidePauseButton();
            _controlsView.ShowPlayButton(RESUME_BUTTON_NAME);
        }

        private IEnumerator CountdownCoroutine(Action onFinished)
        {
            _controlsView.ShowCountdown();

            for (int number = COUNTDOWN_NUMBER; number > 0; number--)
            {
                _controlsView.SetCountdownValue(number.ToString());
                yield return new WaitForSeconds(COUNTDOWN_DELAY_SEC);
            }

            _controlsView.HideCountdown();
            onFinished.SafeInvoke();
        }

        private void OnCountdownFinished()
        {
            _controlsView.ShowPauseButton(PAUSE_BUTTON_NAME);

            if (_gameManager.State == EGameState.NotStarted)
            {
                _gameManager.StartGame();
                return;
            }

            _gameManager.ResumeGame();
        }
    }
}