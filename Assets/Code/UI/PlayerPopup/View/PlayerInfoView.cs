using TMPro;
using UI.PlayerPopup.Presenter;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerPopup.View
{
    public class PlayerInfoView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private XpBarView _xpBarView;

        private readonly CompositeDisposable _compositeDisposable = new();
        private IPlayerInfoPresenter _playerInfoPresenter;

        public void Show(IPlayerInfoPresenter playerInfoPresenter)
        {
            _compositeDisposable.Clear();
            _playerInfoPresenter = playerInfoPresenter;
            _playerInfoPresenter.Icon.Subscribe(OnIconChanged).AddTo(_compositeDisposable);
            _playerInfoPresenter.Level.Subscribe(OnLevelChanged).AddTo(_compositeDisposable);
            _playerInfoPresenter.Description.Subscribe(OnDescriptionChanged).AddTo(_compositeDisposable);
            _xpBarView.Show(_playerInfoPresenter.XpBarPresenter);
        }

        public void Hide()
        {
            _xpBarView.Hide();
            _compositeDisposable.Clear();
            _playerInfoPresenter = null;
        }

        private void OnIconChanged(Sprite icon) => _icon.sprite = icon;

        private void OnLevelChanged(int level) => _level.text = $"{_playerInfoPresenter.LevelLabel}: {level}";

        private void OnDescriptionChanged(string description) => _description.text = description;
    }
}