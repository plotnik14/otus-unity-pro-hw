using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ShootEmUp;
using Utils;
using VContainer.Unity;

namespace UI
{
    public class GameControlsPresenter : IStartable, IDisposable, INonLazy
    {
        private const int COUNTDOWN_NUMBER = 3;
        private const int COUNTDOWN_DELAY_SEC = 1;

        private const string START_BUTTON_NAME = "Start";
        private const string RESUME_BUTTON_NAME = "Resume";
        private const string PAUSE_BUTTON_NAME = "Pause";

        private readonly IGameControlsView _controlsView;
        private readonly IGameManager _gameManager;
        private CancellationTokenSource _cts;

        public GameControlsPresenter(
            IGameControlsView controlsView,
            IGameManager gameManager)
        {
            _controlsView = controlsView;
            _gameManager = gameManager;

            _controlsView.OnPlayButtonClicked += OnPlayButtonClicked;
            _controlsView.OnPauseButtonClicked += OnPauseButtonClicked;
        }

        public void Start() => _controlsView.ShowPlayButton(START_BUTTON_NAME);

        public void Dispose()
        {
            _controlsView.OnPlayButtonClicked -= OnPlayButtonClicked;
            _controlsView.OnPauseButtonClicked -= OnPauseButtonClicked;

            _cts.CancelAndDispose();
            _cts = null;
        }

        private void OnPlayButtonClicked()
        {
            _controlsView.HidePlayButton();

            _cts.CancelAndDispose();
            _cts = new CancellationTokenSource();
            CountdownCoroutineAsync(OnCountdownFinished, _cts.Token).Forget();
        }

        private void OnPauseButtonClicked()
        {
            _gameManager.PauseGame();
            _controlsView.HidePauseButton();
            _controlsView.ShowPlayButton(RESUME_BUTTON_NAME);
        }

        private async UniTaskVoid CountdownCoroutineAsync(Action onFinished, CancellationToken cancellationToken)
        {
            _controlsView.ShowCountdown();

            for (int number = COUNTDOWN_NUMBER; number > 0; number--)
            {
                _controlsView.SetCountdownValue(number.ToString());
                await UniTask.WaitForSeconds(COUNTDOWN_DELAY_SEC, ignoreTimeScale: true, cancellationToken: cancellationToken);
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