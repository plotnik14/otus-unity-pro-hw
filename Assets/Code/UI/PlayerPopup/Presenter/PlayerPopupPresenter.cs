using Player;
using UniRx;

namespace UI.PlayerPopup.Presenter
{
    public class PlayerPopupPresenter : IPlayerPopupPresenter
    {
        private readonly IPlayerInfo _playerInfo;

        public IReadOnlyReactiveProperty<string> Name => _playerInfo.Name;
        public IReadOnlyReactiveProperty<bool> CanLevelUp => _playerInfo.CanLevelUp;
        public string LevelUpButtonLabel => "LEVEL UP"; // localized value
        public IPlayerInfoPresenter PlayerInfoPresenter { get; }
        public IStatsGroupPresenter StatsGroupPresenter { get; }

        public PlayerPopupPresenter(
            IPlayerInfo playerInfo,
            IPlayerInfoPresenter playerInfoPresenter,
            IStatsGroupPresenter statsGroupPresenter)
        {
            _playerInfo = playerInfo;
            PlayerInfoPresenter = playerInfoPresenter;
            StatsGroupPresenter = statsGroupPresenter;
        }

        public void LevelUp() => _playerInfo.LevelUp();
    }
}