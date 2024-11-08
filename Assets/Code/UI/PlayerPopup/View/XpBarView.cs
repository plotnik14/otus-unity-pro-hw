using TMPro;
using UI.PlayerPopup.Presenter;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerPopup.View
{
    public class XpBarView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _progressValueText;
        [SerializeField] private Image _progressValueImage;
        [SerializeField] private Sprite _completedProgressImage;
        [SerializeField] private Sprite _notCompletedProgressImage;

        private IXpBarPresenter _xpBarPresenter;
        private CompositeDisposable _compositeDisposable;

        public void Show(IXpBarPresenter xpBarPresenter)
        {
            _xpBarPresenter = xpBarPresenter;
            _compositeDisposable = new CompositeDisposable();
            _xpBarPresenter.CurrentXpValue.Subscribe(OnXpInfoChanged).AddTo(_compositeDisposable);
        }

        public void Hide()
        {
            _compositeDisposable.Dispose();
            _xpBarPresenter = null;
        }

        private void OnXpInfoChanged(int _)
        {
            int currentXpValue = _xpBarPresenter.CurrentXpValue.Value;
            int requiredXpValue = _xpBarPresenter.RequiredXpValue;
            float progress = (float)currentXpValue / requiredXpValue;

            UpdateBarText(currentXpValue, requiredXpValue);
            UpdateBarImage(progress);
        }

        private void UpdateBarText(int currentXpValue, int requiredXpValue)
        {
            _progressValueText.text = $"XP: {currentXpValue} / {requiredXpValue}";
        }

        private void UpdateBarImage(float progress)
        {
            _progressValueImage.fillAmount = progress;

            _progressValueImage.sprite = progress >= 1f
                ? _completedProgressImage
                : _notCompletedProgressImage;
        }
    }
}