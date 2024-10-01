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
            _controlsView.OnStartResumeButtonClicked += OnStartResumeButtonClicked;
            _controlsView.OnPauseButtonClicked += OnPauseButtonClicked;
        }

        [UsedImplicitly]
        private void Start() =>  _controlsView.ShowStartResumeButton(START_BUTTON_NAME);

        [UsedImplicitly]
        private void OnDestroy()
        {
            _controlsView.OnStartResumeButtonClicked -= OnStartResumeButtonClicked;
            _controlsView.OnPauseButtonClicked -= OnPauseButtonClicked;
        }

        private void OnStartResumeButtonClicked()
        {
            _controlsView.HideStartResumeButton();

            Action onFinished = _gameManager.IsNotStarted
                ? _gameManager.StartGame
                : _gameManager.ResumeGame;

            StartCoroutine(CountdownCoroutine(onFinished));
        }

        private void OnPauseButtonClicked()
        {
            _gameManager.PauseGame();
            _controlsView.HidePauseButton();
            _controlsView.ShowStartResumeButton(RESUME_BUTTON_NAME);
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
            _controlsView.ShowPauseButton(PAUSE_BUTTON_NAME);
            onFinished.SafeInvoke();
        }
    }
}