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

        private CompositeDisposable _compositeDisposable;

        public void Show(IPlayerInfoPresenter playerInfoPresenter)
        {
            _compositeDisposable = new CompositeDisposable();
            playerInfoPresenter.Icon.Subscribe(OnIconChanged).AddTo(_compositeDisposable);
            playerInfoPresenter.Level.Subscribe(OnLevelChanged).AddTo(_compositeDisposable);
            playerInfoPresenter.Description.Subscribe(OnDescriptionChanged).AddTo(_compositeDisposable);
            _xpBarView.Show(playerInfoPresenter.XpBarPresenter);
        }

        public void Hide()
        {
            _xpBarView.Hide();
            _compositeDisposable.Dispose();
        }

        private void OnIconChanged(Sprite icon) => _icon.sprite = icon;

        private void OnLevelChanged(int level) => _level.text = $"Level: {level}";

        private void OnDescriptionChanged(string description) => _description.text = description;
    }
}