using UniRx;

namespace UI.PlayerPopup.Presenter
{
    public interface IXpBarPresenter
    {
        IReadOnlyReactiveProperty<int> CurrentXpValue { get; }
        int RequiredXpValue { get; }
        string XpLabel { get; }
    }
}