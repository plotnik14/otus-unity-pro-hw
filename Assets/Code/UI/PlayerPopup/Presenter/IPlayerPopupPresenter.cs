using UniRx;

namespace UI.PlayerPopup.Presenter
{
    public interface IPlayerPopupPresenter
    {
        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<bool> CanLevelUp { get; }
        IPlayerInfoPresenter PlayerInfoPresenter { get; }
        IStatsGroupPresenter StatsGroupPresenter { get; }
        void LevelUp();
    }
}