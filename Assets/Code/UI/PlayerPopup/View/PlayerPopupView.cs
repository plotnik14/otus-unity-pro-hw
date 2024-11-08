using TMPro;
using UI.PlayerPopup.Presenter;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerPopup.View
{
    public class PlayerPopupView : MonoBehaviour
    {
        private const string LEVEL_UP_LABEL = "LEVEL_UP";

        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private PlayerInfoView _playerInfoView;
        [SerializeField] private StatsGroupView _statsGroupView;
        [SerializeField] private Button _levelUpButton;
        [SerializeField] private TMP_Text _levelUpButtonLabel;
        [SerializeField] private Button _closeButton;

        private IPlayerPopupPresenter _popupPresenter;
        private CompositeDisposable _compositeDisposable;

        public void Show(IPlayerPopupPresenter popupPresenter)
        {
            _popupPresenter = popupPresenter;
            _compositeDisposable = new CompositeDisposable();
            _popupPresenter.Name.Subscribe(OnPlayerNameChanged).AddTo(_compositeDisposable);
            _playerInfoView.Show(_popupPresenter.PlayerInfoPresenter);
            _statsGroupView.Show(_popupPresenter.StatsGroupPresenter);
            _levelUpButtonLabel.text = LEVEL_UP_LABEL;
            _levelUpButton.onClick.AddListener(OnLevelUpClick);
            _popupPresenter.CanLevelUp.Subscribe(OnCanLevelUpChanged).AddTo(_compositeDisposable);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            _playerInfoView.Hide();
            _statsGroupView.Hide();
            _levelUpButton.onClick.RemoveListener(OnLevelUpClick);
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
            _compositeDisposable.Dispose();
            _popupPresenter = null;
        }

        private void OnPlayerNameChanged(string playerName) => _playerName.text = $"@{playerName}";

        private void OnCanLevelUpChanged(bool canLevelUp) => _levelUpButton.interactable = canLevelUp;

        private void OnLevelUpClick() => _popupPresenter.LevelUp();

        private void OnCloseButtonClick() => Hide();
    }
}