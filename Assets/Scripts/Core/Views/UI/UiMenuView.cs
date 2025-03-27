using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Views.UI
{
    /// <summary>
    /// Заготовка для меню
    /// </summary>
    public class UiMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        public event Action OnStartButtonClicked;
        public event Action OnContinueButtonClicked;
        public event Action OnExitButtonClicked;

        [UsedImplicitly]
        private void Awake()
        {
            _startButton.onClick.AddListener(() => OnStartButtonClicked?.Invoke());
            _continueButton.onClick.AddListener(() => OnContinueButtonClicked?.Invoke());
            _exitButton.onClick.AddListener(() => OnExitButtonClicked?.Invoke());
        }

        public void SetStartButtonActive(bool isActive) => _startButton.gameObject.SetActive(isActive);

        public void SetContinueButtonActive(bool isActive) => _continueButton.gameObject.SetActive(isActive);

        public void SetExitButtonActive(bool isActive) => _exitButton.gameObject.SetActive(isActive);
    }
}