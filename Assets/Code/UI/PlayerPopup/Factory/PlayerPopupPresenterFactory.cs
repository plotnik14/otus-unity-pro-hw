using Player;
using UI.PlayerPopup.Presenter;

namespace UI.PlayerPopup.Factory
{
    public class PlayerPopupPresenterFactory
    {
        private readonly IPlayerInfo _playerInfo;

        public PlayerPopupPresenterFactory(IPlayerInfo playerInfo) => _playerInfo = playerInfo;

        public PlayerPopupPresenter CreatePlayerPopupPresenter()
        {
            XpBarPresenter xpBarPresenter = new(_playerInfo);
            PlayerInfoPresenter playerInfoPresenter = new(_playerInfo, xpBarPresenter);
            StatsGroupPresenter statsGroupPresenter = new(_playerInfo);
            PlayerPopupPresenter playerPopupPresenter = new(_playerInfo, playerInfoPresenter, statsGroupPresenter);
            return playerPopupPresenter;
        }
    }
}