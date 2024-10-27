using System;

namespace UI
{
    public interface IGameControlsView
    {
        event Action OnPlayButtonClicked;
        event Action OnPauseButtonClicked;

        void ShowCountdown();
        void HideCountdown();
        void SetCountdownValue(string text);
        void ShowPlayButton(string buttonText);
        void HidePlayButton();
        void ShowPauseButton(string buttonText);
        void HidePauseButton();
    }
}